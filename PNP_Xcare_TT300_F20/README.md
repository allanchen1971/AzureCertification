---
platform: {Windows10}
device: {TT300-F20}
language: {C#}
---

Connect TT300-F20 device to your Azure IoT services
===

---
# Table of Contents

-   [Introduction](#Introduction)
-   [Prerequisites](#Prerequisites)
-   [Prepare the Device](#preparethedevice)
-   [Connect to Azure IoT Central](#ConnecttoCentral)
-   [Integration with Azure IoT Explorer](#IntegrationwithAzureIoTExplorer)
-   [Additional Links](#AdditionalLinks)

<a name="Introduction"></a>

# Introduction 

**About this document**

This document describes how to connect TT300-F20 to Azure IoT Hub using the Azure IoT Explorer with certified device application and device models.

IoT Plug and Play certified device simplifies the process of building devices without custom device code. Using Solution builders can integrated quickly using the certified IoT Plug and Play enabled device based on Azure IoT Central as well as third-party solutions.

This getting started guide provides step by step instruction on getting the device provisioned to Azure IoT Hub using Device Provisioning Service (DPS) and using Azure IoT Explorer to interact with device's capabilities.


TT 300-F30/F31/F32/F33 Key Features:
-   Support 6, 7, 8, 9th Gen. Intel® Core™ i7/i5/i3 socket type processor Intel® H310C PCH
-   2 x HDMI (dual display)
-   4 x USB3.0, 2 x RS232/422/485, 4 x RS232
-   1 x M.2 2280 Key M (storage)
-   1 x M.2 3042 Key B (wireless connectivity)
-   1 x mSATA/miniPCIe (co-lay)
-   1 x PCIe x16
-   2 x PCIe x4
-   1 x SIM card socket
-   Support +12V/24VDC input; support ATX power mode



<a name="Prerequisites"></a>
# Prerequisites

You should have the following items ready before beginning the process:

**For Azure IoT Central**
-   [Azure Account](https://portal.azure.com)
-   [Azure IoT Central application](https://apps.azureiotcentral.com/)


**For Azure IoT Hub**
-   [Azure IoT Hub Instance](https://docs.microsoft.com/en-us/azure/iot-hub/about-iot-hub)
-   [Azure IoT Hub Device Provisioning Service](https://docs.microsoft.com/en-us/azure/iot-dps/quick-setup-auto-provision)
-   [Azure IoT Public Model Repository](https://docs.microsoft.com/en-us/azure/iot-pnp/concepts-model-repository)

<a name="preparethedevice"></a>
# Prepare the Device.

**Hardware Environmental setup**
-   Prepare TT300-F20,and install Win10 IoT Enterprise.
-   Power on the TT300-F20.
-   Connect to the network.

**Software Environmental setup**
-   Download the source code from this GitHub and check the [“PNP_Xcare_TT300_F20”](https://github.com/allanchen1971/AzureCertification/tree/master/PNP_Xcare_TT300_F20) folder
-   Install Visual Studio.
-   Open the project

**Prepare IoT Hub and DPS configuration**
Please refer to this tutorial to complete the following procedures :
1. Use Azure commands or Azure portal to create a Resource Group which include Iot Hub and a Device Provisioning Service
2. To link the DPS instance to your IoT hub
3. To create your device by individual device enrollment in your DPS instance.
4. Make a note of the DPS information (DPS endpoint/Registration ID/ID Scope/Symmetric key).
 
Run the sample
Under the “PNP_Xcare_TT300_F20” folder, open the project and set debug parameter: 
"(-s dps –i {scopeId} –d {deviceID} –k {primarykey} –e {endpoint}"


<a name="ConnecttoCentral"></a>
# Connect to Azure IoT Central
1.	Create an application
Please refer to this [tutorial](https://docs.microsoft.com/en-us/azure/iot-central/core/quick-deploy-iot-central) to create a “Custom application” template.
2.	Create a device template from the device catalog
Please refer to this tutorial to create the [TT300-F20](https://docs.microsoft.com/en-us/azure/iot-central/core/howto-set-up-template#create-a-device-template-from-the-device-catalog) device template.
3.	Add a device
Add a new device under TT300-F20 device template. Make a note of the device ID.
4.	Get connection information
-  ID scope : In your IoT Central application, navigate to Administration > Device Connection. Make a note of the ID scope value.
-  Group primary key : In your IoT Central application, navigate to Administration > Device Connection > SAS-IoT-Devices. Make a note of the shared access signature Primary key value.

![image](https://storageaccountazure9611.blob.core.windows.net/azure-certified/azureCert_Dev_DPS.jpeg)
![image](https://storageaccountazure9611.blob.core.windows.net/azure-certified/azureCert_Dev_DPS_SAS.jpeg)

Use the Cloud Shell to generate a device specific key from the group SAS key you just retrieved using the Azure CLI

az extension add --name azure-iot
az iot central device compute-device-key  --device-id sample-device-01 --pk <the group SAS primary key value>

Make a note of the generated device key, and the ID scope for this application and flash it on the device

<a name="IntegrationwithAzureIoTExplorer"></a>
# Integration with Azure IoT Explorer (Advanced)
This section is optional for Advanced setup

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