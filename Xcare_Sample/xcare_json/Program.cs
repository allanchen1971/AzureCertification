using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCareClient;
using System.Text.Json;
using System.IO;
using System.Threading;

namespace xcare_json
{
    class Program
    {
        public class xcare_Telemetry
        {
            public double cpuTemperature { get; set; }
            public double sysTemperature { get; set; }
        }

        static double TCPU = 0d;
        static double TSYS = 0d;

        static void Main(string[] args)
        {
            init();
            while(true)
            {
                Show_HWM();
                Write_xcare_Telemetry_JsonFile();
                Thread.Sleep(1000);
            }
        }

        static void init()
        {
            try
            {
                UInt32 ret = XCare_EAPI.EApiLibInitialize();
                if ((ret != XCare_EAPI.EAPI_STATUS_SUCCESS) && (ret != XCare_EAPI.EAPI_STATUS_INITIALIZED))
                {
                    Console.WriteLine("EAPI initialize failed!" + " ErrorCode=0x" + Convert.ToString(ret, 16));
                    return;
                }
            }
            catch (Exception except)
            {
                Console.WriteLine("XCare_Monitor_Client error!\r\n" + except.ToString() + "\r\n\r\n" + except.StackTrace);
            }
        }

        static UInt32 GetHWM_TempValue(UInt32 Temp_Id, ref UInt32 val)
        {
            UInt32 capability = 0;
            UInt32 ret = XCare_EAPI.EAPI_STATUS_ERROR;

            ret = XCare_EAPI.EApiHWMGetCaps(Temp_Id, ref capability);
            if (ret != XCare_EAPI.EAPI_STATUS_SUCCESS)
            {
                Console.WriteLine("Initialize_Temp() fail!" + " Error Code=0x" + Convert.ToString(ret, 16));
                return XCare_EAPI.EAPI_STATUS_SUCCESS;

            }
            if (capability != 1)
            {
                Console.WriteLine(Convert.ToString(Temp_Id) + " not support");
                return XCare_EAPI.EAPI_STATUS_SUCCESS;
            }

            if (XCare_EAPI.EApiBoardGetValue(Temp_Id, ref val) == XCare_EAPI.EAPI_STATUS_SUCCESS)
            {
                return XCare_EAPI.EAPI_STATUS_SUCCESS;
            }
            return XCare_EAPI.EAPI_STATUS_ERROR;
        }

        static void Show_HWM()
        {
            double fval = 0.0;
            UInt32 Val = 0;
            const UInt32 KELVINS_OFFSET = 2731;

            GetHWM_TempValue(XCare_EAPI.EAPI_ID_HWMON_CPU_TEMP, ref Val);
            TCPU = (float)(Val - KELVINS_OFFSET) / 10;
            Console.WriteLine($"CPU Temperature {TCPU} C");

            //Sys Temperature
            //GetHWM_TempValue(XCare_EAPI.EAPI_ID_HWMON_SYSTEM_TEMP, ref Val);
            //TSYS = (float)(Val - KELVINS_OFFSET) / 10;
            TSYS = TCPU + 10d;
            Console.WriteLine($"SYS Temperature {TSYS} C");
            
        }

        static void Write_xcare_Telemetry_JsonFile()
        {
            var _xcare_Telemetry = new xcare_Telemetry
            {
                cpuTemperature = TCPU,
                sysTemperature = TSYS,
            };

            var TelemetryJsonString = JsonSerializer.Serialize(_xcare_Telemetry);

            TextWriter writer;
            using (writer = new StreamWriter(@"C:\Xcare\xcare_Telemetry.json", append: false))
            {
                writer.WriteLine(TelemetryJsonString);
                Console.WriteLine($"Write File");
            }
        }

    }
}
