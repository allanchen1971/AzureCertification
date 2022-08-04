using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace XCareClient
{
    static public class DeviceAlert_Defalut
    {
        public static readonly int Default_CPU_Temp_Alert = 80;
        public static readonly int Default_SYS_Temp_Alert = 80;
        public static readonly int Default_HD_RuningHrs_Alert = 50000;
        public static readonly int Default_TotalRuningHrs_Alert = 50000;
        public static readonly int Default_DiskCapacity_Alert = 500;
        public static readonly int Default_HD_Health_Alert = 80;
    }

    static public class DeviceAlert
    {
        public static int CPU_Temp_Alert;
        public static int SYS_Temp_Alert;
        public static Int32 HD_RuningHrs_Alert;
        //public static Int32 TotalRuningHrs_Alert;
        public static Int32 DiskCapacity_Alert;
        public static int HD_Health_Alert;
        public static int EMail_Freqency;
        public static bool Enable_CPU_Temp_Alert;
        public static bool Enable_SYS_Temp_Alert;
        public static bool Enable_HD_RuningHrs_Alert;
        //public static bool Enable_TotalRuningHrs_Alert;
        public static bool Enable_DiskCapacity_Alert;
        public static bool Enable_HD_Health_Alert;
        public static bool Enable_EMail_Alert;
        public static bool bPopUpErrors;
        public static bool bLauch;

    }


    public struct Sent_Client_Info
    {
        public UInt16 ID;
        public UInt16 Length;
        public UInt16 Client_ID;
        public UInt32 OnBoardDate;
        public UInt32 BIOSVersion;
        public UInt32 EAPIVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ModelName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string OS_Type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string AdminName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string AdminPassword;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string IPAddress;
    }

    public struct Sent_Client_Data
    {
        public UInt16 ID;
        public UInt16 Length;
        public UInt16 Client_ID;
        public int CPU_Temp;
        public int SYS_Temp;
        public int HD_Health;
        public int HD_TEMP;
        public UInt32 HD_RuningHrs;
        public UInt64 DiskUsedSpace;
        public UInt64 DiskFreeSpace;
        public UInt64 DiskCapacity;
        public UInt32 GPIO;
    }

    public struct Sent_Client_GPIO
    {
        public UInt16 ID;
        public UInt16 Length;
        public UInt16 Client_ID;
        public UInt32 Data;
    }

    class XCare_EAPI
   {
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiLibInitialize();
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiLibUnInitialize();
           [DllImport("EAPI_1.dll")]
           public static unsafe extern UInt32 EApiBoardGetStringA(UInt32 Id, byte* pBuffer, UInt32* pBufLen);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiBoardGetValue(UInt32 Id, ref UInt32 pValue);

           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiHWMGetFanCount(ref UInt32 p_fan_count);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiHWMGetCaps(UInt32 Id, ref UInt32 pCapability);
//VGA
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaGetBacklightEnable(UInt32 Id, ref UInt32 pEnable);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaSetBacklightEnable(UInt32 Id, UInt32 Enable);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaSetPolarity(UInt32 Id, UInt32 dwSetting);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaSetFrequency(UInt32 Id, UInt32 Level);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaGetBacklightLevel(UInt32 Id, ref UInt32 pLevel);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaSetBacklightLevel(UInt32 Id, UInt32 Level);
           [DllImport("EAPI_1.dll")]
           public static unsafe extern UInt32 EApiVgaGetBacklightBrightness(UInt32 Id, ref UInt32 pBright);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiVgaSetBacklightBrightness(UInt32 Id, UInt32 Bright);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 Xcare_VGA_VgaGetCaps(UInt32 Id, UInt32 ItemId,ref UInt32 value);
           
        //WDG
           [DllImport("EAPI_1.dll")]
           public static unsafe extern UInt32 EApiWDogGetCap(UInt32* pMaxDelay, UInt32* pMaxEventTimeout, UInt32* pMaxResetTimeout);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiWDogSetEventType(UInt32 EventType);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiWDogStart(UInt32 Delay, UInt32 EventTimeout, UInt32 ResetTimeout);
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiWDogTrigger();
           [DllImport("EAPI_1.dll")]
           public static extern UInt32 EApiWDogStop();
           [DllImport("EAPI_1.dll")]
           public unsafe static extern void SetCallback(IntPtr fnCallBack);
           [DllImport("EAPI_1.dll")]
           public unsafe static extern bool SetCallbackEx(IntPtr fnCallBack);

//HWM Enum
           // Temperature flag
           public enum TempList : int
           {
               TCPU = 0,   // CPU temperature
               TSYS,       // System temperature
               TAUX,       // 3'rd thermal dioad
               TCount      // Only for detect temperature count
           }

           // Fan speed flag
           public enum FanList : int
           {
               FCPU = 0,   // CPU fan speed
               FSYS,       // System fan speed
               F3RD,       // Other fan speed
               FCount      // Only for detect fan count
           }

           // Voltage flag
           public enum VoltList : int
           {
               VCORE = 0,
               V25,
               V33,
               VBAT,
               V50,
               V5SB,
               V120,
               VCount      // Only for detect voltage count
           }

           // Voltage flag
           public enum OtherList : int
           {
               OCURRENT = 0,
               OPOWER,
               OCount
           }

       #region EAPI_Number
       //Define EAPI Status number
       // (0)											
       public const UInt32 EAPI_STATUS_SUCCESS = 0x00000000;
       // (0xFFFFF000~0xFFFFF0FF)
       public const UInt32 EAPI_STATUS_ERROR = 0xFFFFF0FF;

       // (0xFFFFF100~0xFFFFF1FF)
       public const UInt32 EAPI_STATUS_SW_TIMEOUT = 0xFFFFF1FF;
       public const UInt32 EAPI_STATUS_HW_TIMEOUT = 0xFFFFF1FE;


       public const UInt32 EAPI_ID_BOARD_MANUFACTURER_STR = 0x00000000;
       public const UInt32 EAPI_ID_BOARD_NAME_STR = 0x00000001;
       public const UInt32 EAPI_ID_BOARD_REVISION_STR = 0x00000002;
       public const UInt32 EAPI_ID_BOARD_SERIAL_STR = 0x00000003;
       public const UInt32 EAPI_ID_BOARD_BIOS_REVISION_STR = 0x00000004;
       public const UInt32 EAPI_ID_BOARD_HW_REVISION_STR = 0x00000005;
       public const UInt32 EAPI_ID_BOARD_PLATFORM_TYPE_STR = 0x00000006;

       public const UInt32 EAPI_ID_GET_EAPI_SPEC_VERSION = 0x00000000;
       public const UInt32 EAPI_ID_BOARD_BOOT_COUNTER_VAL = 0x00000001;
       public const UInt32 EAPI_ID_BOARD_RUNNING_TIME_METER_VAL = 0x00000002;
       public const UInt32 EAPI_ID_BOARD_PNPID_VAL = 0x00000003;
       public const UInt32 EAPI_ID_BOARD_PLATFORM_REV_VAL = 0x00000004;
       public const UInt32 EAPI_ID_BOARD_DRIVER_VERSION_VAL = 0x00010000;
       public const UInt32 EAPI_ID_BOARD_LIB_VERSION_VAL = 0x00010001;
       public const UInt32 EAPI_ID_BOARD_FIRMWARE_VERSION_VAL = 0x00010002;

       public const UInt32 EAPI_ID_HWMON_GAP_TEMP = 0x00000001;
       public const UInt32 EAPI_ID_HWMON_CPU_TEMP = 0x00020000;
       public const UInt32 EAPI_ID_HWMON_CHIPSET_TEMP = 0x00020001;
       public const UInt32 EAPI_ID_HWMON_SYSTEM_TEMP = 0x00020002;
       public const UInt32 EAPI_ID_HWMON_CPU_TEMP1 = 0x00020010;
       public const UInt32 EAPI_ID_HWMON_CHIPSET_TEMP1 = 0x00020011;
       public const UInt32 EAPI_ID_HWMON_SYSTEM_TEMP1 = 0x00020012;
       public const UInt32 EAPI_ID_HWMON_CPU_TEMP2 = 0x00020020;
       public const UInt32 EAPI_ID_HWMON_CHIPSET_TEMP2 = 0x00020021;
       public const UInt32 EAPI_ID_HWMON_SYSTEM_TEMP2 = 0x00020022;
       public const UInt32 EAPI_ID_HWMON_CPU_TEMP3 = 0x00020030;
       public const UInt32 EAPI_ID_HWMON_CHIPSET_TEMP3 = 0x00020031;
       public const UInt32 EAPI_ID_HWMON_SYSTEM_TEMP3 = 0x00020032;

       public const UInt32 EAPI_ID_HWMON_VOLTAGE_GAP = 0x00000004;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_VCORE = 0x00021004;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_2V5 = 0x00021008;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_3V3 = 0x0002100C;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_VBAT = 0x00021010;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_5V = 0x00021014;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_5VSB = 0x00021018;
       public const UInt32 EAPI_ID_HWMON_VOLTAGE_12V = 0x0002101C;

       public const UInt32 EAPI_ID_HWMON_GAP_FAN = 0x00000001;
       public const UInt32 EAPI_ID_HWMON_FAN_CPU = 0x00022000;
       public const UInt32 EAPI_ID_HWMON_FAN_SYSTEM = 0x00022001;
       public const UInt32 EAPI_ID_HWMON_FAN_THIRD = 0x00022002;

       public const UInt32 EAPI_ID_HWMON_OTHER_GAP = 0x00000001;
       public const UInt32 EAPI_ID_HWMON_CURRENT = 0x00023000;

       public const UInt32 EAPI_ID_BACKLIGHT_1 = 0x00000000;
       public const UInt32 EAPI_ID_BACKLIGHT_2 = 0x00000001;
       public const UInt32 EAPI_ID_BACKLIGHT_3 = 0x00000002;
       public const UInt32 EAPI_ID_BACKLIGHT_F = 0x0000000F;

       public const UInt32 EAPI_BACKLIGHT_SET_ON = 0x00000000;
       public const UInt32 EAPI_BACKLIGHT_SET_OFF = 0xFFFFFFFF;
//WDG
       public const UInt32 EAPI_WDOG_EVENT_NONE = 0;
       public const UInt32 EAPI_WDOG_EVENT_IRQ = 1;
       public const UInt32 EAPI_WDOG_EVENT_SCI = 2;
       public const UInt32 EAPI_WDOG_EVENT_BTN = 3;

       // Status
       // (0xFFFFFF00~0xFFFFFFFF)
       public const UInt32 EAPI_STATUS_NOT_INITIALIZED = 0xFFFFFFFF;
       public const UInt32 EAPI_STATUS_INITIALIZED = 0xFFFFFFFE;
       public const UInt32 EAPI_STATUS_ALLOC_ERROR = 0xFFFFFFFD;
       public const UInt32 EAPI_STATUS_DRIVER_TIMEOUT = 0xFFFFFFFC;
       // (0xFFFFFE00~0xFFFFFEFF)
       public const UInt32 EAPI_STATUS_INVALID_PARAMETER = 0xFFFFFEFF;
       public const UInt32 EAPI_STATUS_INVALID_BLOCK_ALIGNMENT = 0xFFFFFEFE;
       public const UInt32 EAPI_STATUS_INVALID_BLOCK_LENGTH = 0xFFFFFEFD;
       public const UInt32 EAPI_STATUS_INVALID_DIRECTION = 0xFFFFFEFC;
       public const UInt32 EAPI_STATUS_INVALID_BITMASK = 0xFFFFFEFB;
       public const UInt32 EAPI_STATUS_RUNNING = 0xFFFFFEFA;
       // (0xFFFFFC00~0xFFFFFCFF)
       public const UInt32 EAPI_STATUS_UNSUPPORTED = 0xFFFFFCFF;
       // (0xFFFFFB00~0xFFFFFBFF)
       public const UInt32 EAPI_STATUS_NOT_FOUND = 0xFFFFFBFF;
       public const UInt32 EAPI_STATUS_TIMEOUT = 0xFFFFFBFE;
       public const UInt32 EAPI_STATUS_BUSY_COLLISION = 0xFFFFFBFD;
       // (0xFFFFFA00~0xFFFFFAFF)
       public const UInt32 EAPI_STATUS_READ_ERROR = 0xFFFFFAFF;
       public const UInt32 EAPI_STATUS_WRITE_ERROR = 0xFFFFFAFE;
       // (0xFFFFF900~0xFFFFF9FF)
       public const UInt32 EAPI_STATUS_MORE_DATA = 0xFFFFF9FF;

        //Xcare Define
        public const UInt16 Xcare_Sent_Info_Data = 1;
        public const UInt16 Xcare_Sent_HWM_Data = 2;
        public const UInt16 Xcare_Sent_GPIO_Data = 3;



       #endregion
   }

   static public class EAPI_EC_GPIO
    {
        [DllImport("EAPI_1.dll")]
        public static extern UInt32 EApiGPIOGetCount(ref UInt32 pIOcount);
        [DllImport("EAPI_1.dll")]
        public static extern UInt32 EApiGPIOGetDirectionCaps(UInt32 Id, ref UInt32 pInputs, ref UInt32 pOutputs);
        [DllImport("EAPI_1.dll")]
        public static extern UInt32 EApiGPIOGetDirection(UInt32 Id, UInt32 Bitmask, ref UInt32 pDirection);
        [DllImport("EAPI_1.dll")]
        public static extern UInt32 EApiGPIOSetDirection(UInt32 Id, UInt32 Bitmask, UInt32 Direction);
        [DllImport("EAPI_1.dll")]
        public static extern UInt32 EApiGPIOGetLevel(UInt32 Id, UInt32 Bitmask, ref UInt32 pLevel);
        [DllImport("EAPI_1.dll")]
        public static extern UInt32 EApiGPIOSetLevel(UInt32 Id, UInt32 Bitmask, UInt32 Level);

        public const UInt32 EAPI_GPIO_ID0 = 0;
        public const UInt32 EAPI_GPIO_ID1 = 1;
        public const UInt32 EAPI_GPIO_ID2 = 2;
        public const UInt32 EAPI_GPIO_ID3 = 3;
        public const UInt32 EAPI_GPIO_ID4 = 4;
        public const UInt32 EAPI_GPIO_ID5 = 5;
        public const UInt32 EAPI_GPIO_ID6 = 6;
        public const UInt32 EAPI_GPIO_ID7 = 7;
        public const UInt32 EAPI_ID_GPIO_BANK00 = 0x00010000;
        public const UInt32 EAPI_ID_GPIO_BANK01 = 0x00010001;
        public const UInt32 EAPI_ID_GPIO_BANK02 = 0x00010002;
    }

}
