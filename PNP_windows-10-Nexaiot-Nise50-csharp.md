---
platform: {Windows10}
device: {Nise50}
language: {C#}
---

Connect Nise50 device to your Azure IoT services
===

---
# Table of Contents

-   [Introduction](#Introduction)
-   [Prerequisites](#Prerequisites)
-   [Prepare the Device](#preparethedevice)
-   [Integration with Azure IoT Explorer](#IntegrationwithAzureIoTExplorer)
-   [Additional Links](#AdditionalLinks)

<a name="Introduction"></a>

# Introduction 

**About this document**

This document describes how to connect [Nexaiot Nise50] to Azure IoT Hub using the Azure IoT Explorer with certified device application and device models.

IoT Plug and Play certified device simplifies the process of building devices without custom device code. Using Solution builders can integrated quickly using the certified IoT Plug and Play enabled device based on Azure IoT Central as well as third-party solutions.

This getting started guide provides step by step instruction on getting the device provisioned to Azure IoT Hub using Device Provisioning Service (DPS) and using Azure IoT Explorer to interact with device's capabilities.

[Nexaiot Nise50] is powered by the latest generation of Intel® Atom™ processor E3826 (formerly codenamed "Bay Trail-I"), NISE 50 series positions at the intelligent IoT gateway for factory automation and for smart city applications. Up to 4G on-board DDR3L memory, the NISE 50 series support operating temperature from -5 up to 55 degree C with 24V DC input with +/-20% range. The NISE 50 series have strong connectivity - Ethernet-based LAN port and traditional RS485, mainly for Modbus TCP or Modbus RTU communication. For wireless connectivity, there are 3x mini-PCIe sockets which can support optional wireless modules for IoT applications, for example, Wi-Fi, Bluetooth, 3.5G and 4G LTE module. NISE 50 is definitely the best choice for M2M intelligent system as an intelligent IoT gateway.

<a name="Prerequisites"></a>
# Step 1: Prerequisites

You should have the following items ready before beginning the process:

-   [Azure Account](https://portal.azure.com)
-   [Azure IoT Hub Instance](https://docs.microsoft.com/en-us/azure/iot-hub/about-iot-hub)
-   [Azure IoT Hub Device Provisioning Service](https://docs.microsoft.com/en-us/azure/iot-dps/quick-setup-auto-provision)
-   [Azure IoT Public Model Repository](https://docs.microsoft.com/en-us/azure/iot-pnp/concepts-model-repository)

<a name="preparethedevice"></a>
# Step 2: Prepare your Device

-   Connect the power adapter, USB Keyborad/Mouse with Embux EAS-50.
-   Wait until the operating system is ready.

<a name="Build"></a>
# Step 3: Build and Run the sample

-   Download the [Azure IoT SDK](https://github.com/Azure/azure-iot-sdk-csharp) and the sample programs and save them to your local repository.
-   Start a new instance of Visual Studio 2019.
-   Open the **iothub\_csharp\_client.sln** solution in the `device` folder in your local copy of the repository.
-   In Visual Studio, from Solution Explorer, navigate to the **samples** folder.
-   In the **DeviceClientAmqpSample** project, open the ***Program.cs*** file.
-   Locate the following code in the file:

        private const string DeviceConnectionString = "<replace>";
        
-   Replace `<replace>` with the connection string for your device.
-   In **Solution Explorer**, right-click the **DeviceClientAmqpSample** project, click **Debug**, and then click **Start new instance** to build and run the sample. The console displays messages as the application sends device-to-cloud messages to IoT Hub.
-   Use the **DeviceExplorer** utility to observe the messages IoT Hub receives from the **Device Client AMQP Sample** application.
-   Refer "Monitor device-to-cloud events" in [DeviceExplorer Usage document](https://github.com/Azure/azure-iot-sdk-csharp/blob/master/tools/DeviceExplorer/doc/how_to_use_device_explorer.md) to see the data your device is sending.
-   Refer "Send cloud-to-device messages" in [DeviceExplorer Usage document](https://github.com/Azure/azure-iot-sdk-csharp/blob/master/tools/DeviceExplorer/doc/how_to_use_device_explorer.md) for instructions on sending messages to device.

**Development Environmental setup**

IoT Plug and Play Certification is certifying specific device code implementation against specific device model. Device builders should either pre-install device code or make the binary download-able.{Please include the below pointers specific to device in this section and add screen shots where ever necessary}

1.	Describing the capabilities of the device 
2.	How to setup the device and connect power
3.	How to take the DPS configuration and program the device (Note : DPS ID scope should be configured w/o recompiling the embedded code)
4.	How to configure device over Wifi, cellular, screens, etc.
5.	Add the links of external software/tools as required 
6.	Add steps on how to run the device code/how and where to download binary and then run on device. If you have multiple options on how to deploy device code please mention only one option here and other options in Additional links section

<a name="IntegrationwithAzureIoTExplorer"></a>
# Integration with Azure IoT Explorer

-   Include the steps on how to connect the IoT Plug and Play Device to Azure IoT Explorer
-   Include screenshots and comments on how IoT Explorer shows/visualize telemetry , commands and properties coming from your IoT Plug and Play device.
-   Include the steps on how to interact with devices (telemetry, commands properties)
-   Ensure to attach the screenshot on consuming the device models available in public repository (not local folder) when using Azure IoT Explorer

# Additional information
Put any additional information here such as alternative paths to deploy device application etc.

<a name="AdditionalLinks"></a>
# Additional Links

Please refer to the below link for additional information for Plug and Play 

-   [Manage cloud device messaging with Azure-IoT-Explorer](https://github.com/Azure/azure-iot-explorer/releases)
-   [Import the Plug and Play model](https://docs.microsoft.com/en-us/azure/iot-pnp/concepts-model-repository)
-   [Configure to connect to IoT Hub](https://docs.microsoft.com/en-us/azure/iot-pnp/quickstart-connect-device-c)
-   [How to use IoT Explorer to interact with the device ](https://docs.microsoft.com/en-us/azure/iot-pnp/howto-use-iot-explorer#install-azure-iot-explorer)   
[Nexaiot Nise50]: https://www.nexcom.com.tw/Products/industrial-computing-solutions/industrial-fanless-computer/atom-compact/fanless-nise-50-iot-gateway