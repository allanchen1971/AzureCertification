---
platform: {Windows10}
device: { NISE4300}
language: {C#}
---

Connect NISE4300 device to your Azure IoT services
===

---
# Table of Contents

-   [Introduction](#Introduction)
-   [Prerequisites](#Prerequisites)
-   [Prepare your Device](#preparethedevice)
-   [Prepare your DPS and iot hub](#GetDPSInformation)
-   [Build and Run the sample](#BuildRunSample)
-   [Integration with Azure IoT Explorer](#IntegrationwithAzureIoTExplorer)
-   [Additional Links](#AdditionalLinks)

<a name="Introduction"></a>

# Introduction 

**About this document**

This document describes how to connect [Nexaiot NISE4300]( https://www.nexaiot.com/tw/product/Industrial%20PC/Hi-performance%20NISE%20Fanless%20PC/NISE4300) to Azure IoT Hub using the Azure IoT Explorer with certified device application and device models.

IoT Plug and Play certified device simplifies the process of building devices without custom device code. Using Solution builders can integrated quickly using the certified IoT Plug and Play enabled device based on Azure IoT Central as well as third-party solutions.

This getting started guide provides step by step instruction on getting the device provisioned to Azure IoT Hub using Device Provisioning Service (DPS) and using Azure IoT Explorer to interact with device's capabilities.

[Nexaiot NISE4300]( https://www.nexaiot.com/tw/product/Industrial%20PC/Hi-performance%20NISE%20Fanless%20PC/NISE4300) is Integrated with 6th generation Intel® Core™ i5/i3 processors, NISE 4300 is fanless PC designed for industrial applications which demand high CPU and graphics performance and adaptable for panel PC. NISE 4300 supports up to 16G DDR3L memory and have several options on storage devices like M.2, HDD, mSATA or SSD. NISE 4300 supports DC input of 24 VDC,+/-20% range and can be operated in an extended operating temperature range between 0 to 55 Celsius degree. For extended module availability, NISE 4300 also designed one internal mini-PCIe socket to support IoT applications (integrate with optional GbE LAN, Wi-Fi module) and common communication applications (integrate with optional GPIO, RS232/422/485 module).

<a name="Prerequisites"></a>
# Step 1: Prerequisites

You should have the following items ready before beginning the process:

-   [Azure Account](https://portal.azure.com)
-   [Azure IoT Hub Instance](https://docs.microsoft.com/en-us/azure/iot-hub/about-iot-hub)
-   [Azure IoT Hub Device Provisioning Service](https://docs.microsoft.com/en-us/azure/iot-dps/quick-setup-auto-provision)
-   [Azure IoT Public Model Repository](https://docs.microsoft.com/en-us/azure/iot-pnp/concepts-model-repository)

<a name="preparethedevice"></a>
# Step 2: Prepare your Device

-   Connect the power adapter, USB Keyborad/Mouse with [Nexaiot NISE4300]( https://www.nexaiot.com/tw/product/Industrial%20PC/Hi-performance%20NISE%20Fanless%20PC/NISE4300).
-   Wait until the operating system is ready.

<a name="GetDPSInformation"></a>
# Step 3: Prepare your DPS and iot hub

-   Connect to the Azure portal and Create [Azure IOT Hub Device Provisioning Services](https://docs.microsoft.com/en-us/azure/iot-dps/quick-setup-auto-provision) and [Azure IoT Hub Instance](https://docs.microsoft.com/en-us/azure/iot-hub/about-iot-hub)
-   Please keep the DPS information (ID Scope/Global device endpoint/Device Key).
-   Please Create a device under [Azure IoT Hub Instance](https://docs.microsoft.com/en-us/azure/iot-hub/about-iot-hub) and keep the device ID.

<a name="BuildRunSample"></a>
# Step 4: Build and Run the sample

-   Download the [Xcare SDK](https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_NISE4300) and the sample programs and save them to your local repository.
-   Start a new instance of Visual Studio 2019.
-   Open the **xcarePNP.csproj** solution in your local copy of the repository.
-   In **Solution Explorer**, right-click and choose **Build** for build this project.
-   right-click the **XcarePNP** project, click **Debug**, and then add run parameter : "-s dps -i {DPS ID Scope} -d {Device ID} -k {DeviceKey} -e {Global device endpoint}"
-   click **Start new instance** to build and run the sample. The console displays messages as the application sends device-to-cloud messages to IoT Hub.

<a name="IntegrationwithAzureIoTExplorer"></a>
# Integration with Azure IoT Explorer

-   Use the **DeviceExplorer** utility and Click **IoT Plug and Play components**
-   (Step1) On the **Model ID** field to fill **dtmi:nexcom:NISE4300;1**
-   (Step2) You can add **Public Repositiory** or Choose **Local Folder** (Path on Models in your local copy of the repository.
-   (Step3) Click **Components"->**Default component**
-   Refer [IOT Plug and Play components]( https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_NISE4300/Picture/PNP1.jpg)
-   You can see the device **Information\Properties(read-only)\Properties(writable)\Commands\Telemetry**
-   Refer [IOT Plug and Play components Interface]( https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_NISE4300/Picture/PNP2.jpg) to see the your device Interface.
-   Refer [IOT Plug and Play components Properiteies]( https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_NISE4300/Picture/PNP3.jpg) to see the your device Properitieies.
-   Refer [IOT Plug and Play components Properiteies (writable)]( https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_NISE4300/Picture/PNP4.jpg) to see the your device Properitieies(writable).
-   Refer [IOT Plug and Play components Command]( https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_NISE4300/Picture/pnp5.jpg) to sent your reboot command.
-   Under **Telemetry** property and press **Start** to observe the messages IoT Hub receives from the application.


<a name="AdditionalLinks"></a>
# Additional Links

Please refer to the below link for additional information for Plug and Play 
-   [Manage cloud device messaging with Azure-IoT-Explorer](https://github.com/Azure/azure-iot-explorer/releases)
-   [Import the Plug and Play model](https://docs.microsoft.com/en-us/azure/iot-pnp/concepts-model-repository)
-   [Configure to connect to IoT Hub](https://docs.microsoft.com/en-us/azure/iot-pnp/quickstart-connect-device-c)
-   [How to use IoT Explorer to interact with the device ](https://docs.microsoft.com/en-us/azure/iot-pnp/howto-use-iot-explorer#install-azure-iot-explorer)   
-   [Nexaiot NISE4300]( https://www.nexaiot.com/tw/product/Industrial%20PC/Hi-performance%20NISE%20Fanless%20PC/NISE4300)
