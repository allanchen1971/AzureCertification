// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.IO;

namespace Thermostat
{
    internal enum StatusCode
    {
        Completed = 200,
        InProgress = 202,
        NotFound = 404,
        BadRequest = 400
    }

    public class ThermostatSample
    {
        public class xcare_Telemetry
        {
            public double cpuTemperature { get; set; }
            public double sysTemperature { get; set; }
        }
        public class xcare_Properties
        {
            public string modelname { get; set; }
            public int GPIO { get; set; }
            public int workingSet { get; set; }
        }

        private readonly Random _random = new Random();

        //private double _temperature = 0d;
        private double CPU_temperature = 0d;
        private double SYS_temperature = 0d;
        private double _maxTemp = 0d;
        private int _GPIOValue = 0;
        private bool bInitial = false;

        // Dictionary to hold the temperature updates sent over.
        // NOTE: Memory constrained devices should leverage storage capabilities of an external service to store this information and perform computation.
        // See https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services for more details.
        private readonly Dictionary<DateTimeOffset, double> _temperatureReadingsDateTimeOffset = new Dictionary<DateTimeOffset, double>();

        private readonly DeviceClient _deviceClient;
        private readonly ILogger _logger;

        public ThermostatSample(DeviceClient deviceClient, ILogger logger)
        {
            _deviceClient = deviceClient ?? throw new ArgumentNullException($"{nameof(deviceClient)} cannot be null.");
            _logger = logger ?? LoggerFactory.Create(builer => builer.AddConsole()).CreateLogger<ThermostatSample>();
        }

        public async Task PerformOperationsAsync(CancellationToken cancellationToken)
        {
            // This sample follows the following workflow:
            // -> Set handler to receive "targetTemperature" updates, and send the received update over reported property.
            // -> Set handler to receive "getMaxMinReport" command, and send the generated report as command response.
            // -> Periodically send "temperature" over telemetry.
            // -> Send "maxTempSinceLastReboot" over property update, when a new max temperature is set.

            _logger.LogDebug($"Set handler to receive \"targetGPIO\" updates.");
            await _deviceClient.SetDesiredPropertyUpdateCallbackAsync(TargetGPIOUpdateCallbackAsync, _deviceClient, cancellationToken);

            _logger.LogDebug($"Set handler for \"getMaxMinReport\" command.");
            await _deviceClient.SetMethodHandlerAsync("NexDeviceInfo1*reboot", HandleRebootCommandAsync, _deviceClient, cancellationToken);

            bool temperatureReset = true;
            await Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (temperatureReset)
                    {
                        using (StreamReader r = new StreamReader(@"C:\Xcare\xcare_Telemetry.json"))
                        {
                            string json = r.ReadToEnd();
                            var Telemetry = System.Text.Json.JsonSerializer.Deserialize<xcare_Telemetry>(json);
                            CPU_temperature = Telemetry.cpuTemperature;
                            SYS_temperature = Telemetry.sysTemperature;
                        }
                    }
                    await SendTemperatureAsync();
                    await Task.Delay(10 * 1000);
                }
            });
        }

        // The desired property update callback, which receives the target temperature as a desired property update,
        // and updates the current temperature value over telemetry and reported property update.
        private async Task TargetGPIOUpdateCallbackAsync(TwinCollection desiredProperties, object userContext)
        {
            string propertyName = "targetGPIO";
            JObject targetTempJson = desiredProperties["NexDeviceInfo1"];
            TwinCollection test = new TwinCollection(targetTempJson.ToString());            
            (bool targetTempUpdateReceived, int targetGPIO) = GetPropertyFromTwin<int>(test, propertyName);
            if (targetTempUpdateReceived)
            {
                _logger.LogDebug($"Property: Received - {{ \"{propertyName}\": {targetGPIO}}}.");
                //a01
                TwinCollection reportedProperties = new TwinCollection();
                TwinCollection component = new TwinCollection();
                TwinCollection ackProps = new TwinCollection();
                component["__t"] = "c"; // marker to identify a component
                ackProps["value"] = targetGPIO;
                ackProps["ac"] = 200; // using HTTP status codes
                ackProps["av"] = desiredProperties.Version; // not read from a desired property
                ackProps["ad"] = "Successfully updated target GPIO";
                component[propertyName] = ackProps;
                reportedProperties["NexDeviceInfo1"] = component;
                await _deviceClient.UpdateReportedPropertiesAsync(reportedProperties);

                _logger.LogDebug($"Property: Update - {{\"{propertyName}\": {targetGPIO} }} is {StatusCode.Completed}.");
            }
            else
            {
                _logger.LogDebug($"Property: Received an unrecognized property update from service.");
            }
        }

        private static (bool, T) GetPropertyFromTwin<T>(TwinCollection collection, string propertyName)
        {
            return collection.Contains(propertyName) ? (true, (T)collection[propertyName]) : (false, default);
        }
        // The callback to handle "getMaxMinReport" command. This method will returns the max, min and average temperature from the specified time to the current time.
        private async Task<MethodResponse> HandleRebootCommandAsync(MethodRequest request, object userContext)
        {
            try
            {
                int DelayTime = JsonConvert.DeserializeObject<int>(request.DataAsJson);
                _logger.LogDebug($"Command: Received - Set Reboot Timer Delaytime = {DelayTime}.");
                
                var report = new
                {
                    Status = "Success",
                };

                byte[] responsePayload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
                _logger.LogDebug($"!!System will shutdown after {DelayTime} sec.");
                //
                return await Task.FromResult(new MethodResponse(responsePayload, (int)StatusCode.Completed));
            }
            catch (JsonReaderException ex)
            {
                _logger.LogDebug($"Command input is invalid: {ex.Message}.");
                return await Task.FromResult(new MethodResponse((int)StatusCode.BadRequest));
            }
        }

        // Send temperature updates over telemetry. The sample also sends the value of max temperature since last reboot over reported property update.
        private async Task SendTemperatureAsync()
        {
            await SendTemperatureTelemetryAsync();
            if(!bInitial)
            {
                await UpdatePropertyUpdate();
                bInitial = true;
            }            
        }

        // Send temperature update over telemetry.
        private async Task SendTemperatureTelemetryAsync()
        {
            string CPUtelemetryName = "cpuTemperature";
            string SYStelemetryName = "sysTemperature";

            string telemetryPayload = $"{{ \"{CPUtelemetryName}\": {CPU_temperature},\"{SYStelemetryName}\": {SYS_temperature}  }}";
            var message = new Message(Encoding.UTF8.GetBytes(telemetryPayload));
            message.ComponentName = "NexDeviceInfo1";
            message.ContentType = "application/json";
            message.ContentEncoding = "utf-8";
            await _deviceClient.SendEventAsync(message);
            _logger.LogDebug($"Telemetry: Sent - {{ \"{CPUtelemetryName}\": {CPU_temperature}°C, \"{SYStelemetryName}\": {SYS_temperature}°C }}.");
        }

        private async Task UpdatePropertyUpdate()
        {
            string propertyName_modelname = "modelname";
            string propertyName_GPIO = "GPIO";
            string propertyName_workingSet = "workingSet";

            using (StreamReader r = new StreamReader(@"C:\Xcare\xcare_Properties.json"))
            {
                string json = r.ReadToEnd();
                var Properties = System.Text.Json.JsonSerializer.Deserialize<xcare_Properties>(json);
                Console.WriteLine($"modelname : {Properties.modelname}");
                Console.WriteLine($"GPIO : {Properties.GPIO}");
                Console.WriteLine($"workingSet : {Properties.workingSet}");

                TwinCollection reportedProperties = new TwinCollection();
                TwinCollection component = new TwinCollection();
                component["__t"] = "c";
                component[propertyName_modelname] = Properties.modelname;
                component[propertyName_GPIO] = Properties.GPIO;
                component[propertyName_workingSet] = Properties.workingSet;
                reportedProperties["NexDeviceInfo1"] = component;
                await _deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
            }
        }
    }
}
