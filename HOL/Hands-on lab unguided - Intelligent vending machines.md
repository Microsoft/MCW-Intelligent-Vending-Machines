![](images/HeaderPic.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Intelligent vending machines
</div>

<div class="MCWHeader2">
Hands-on lab unguided
</div>

<div class="MCWHeader3">
March 2018
</div>


Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.
© 2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**
<!-- TOC -->

- [Intelligent vending machines hands-on lab unguided](#intelligent-vending-machines-hands-on-lab-unguided)
    - [Abstract and learning objectives](#abstract-and-learning-objectives)
    - [Overview](#overview)
    - [Solution architecture](#solution-architecture)
    - [Requirements](#requirements)
    - [Before the hands-on lab](#before-the-hands-on-lab)
        - [Task 1: Provision an R Server on HDInsight with Spark cluster](#task-1-provision-an-r-server-on-hdinsight-with-spark-cluster)
        - [Task 2: Setup a lab virtual machine (VM)](#task-2-setup-a-lab-virtual-machine-vm)
        - [Task 3: Install Power BI Desktop on the lab VM](#task-3-install-power-bi-desktop-on-the-lab-vm)
        - [Task 4: Prepare an SSH client](#task-4-prepare-an-ssh-client)
        - [Task 5: Install R Tools for Visual Studio 2017](#task-5-install-r-tools-for-visual-studio-2017)
    - [Exercise 1: Environment setup](#exercise-1-environment-setup)
        - [Task 1: Download and open the vending machines starter project](#task-1-download-and-open-the-vending-machines-starter-project)
        - [Task 2: Provision IoT Hub](#task-2-provision-iot-hub)
        - [Task 3: Create Microsoft Machine Learning Server on Linux](#task-3-create-microsoft-machine-learning-server-on-linux)
        - [Task 4: Create Storage Account](#task-4-create-storage-account)
        - [Task 5: Provision Cognitive Services Face API](#task-5-provision-cognitive-services-face-api)
        - [Task 6: Provision SQL Database](#task-6-provision-sql-database)
    - [Exercise 2: Create Dynamic Pricing Model](#exercise-2-create-dynamic-pricing-model)
        - [Task 1: Create a model locally](#task-1-create-a-model-locally)
        - [Task 2: Try a prediction locally](#task-2-try-a-prediction-locally)
        - [Task 3: Create the model in R Server on HDInsight](#task-3-create-the-model-in-r-server-on-hdinsight)
        - [Task 4: Create predictive service in R Server Operationalization](#task-4-create-predictive-service-in-r-server-operationalization)
    - [Exercise 3: Implement dynamic pricing](#exercise-3-implement-dynamic-pricing)
        - [Task 1: Implement photo uploads to Azure Storage](#task-1-implement-photo-uploads-to-azure-storage)
        - [Task 2: Invoke Face API](#task-2-invoke-face-api)
        - [Task 3: Invoke pricing model](#task-3-invoke-pricing-model)
        - [Task 4: Configure the Simulator](#task-4-configure-the-simulator)
        - [Task 5: Test dynamic pricing in Simulator](#task-5-test-dynamic-pricing-in-simulator)
    - [Exercise 4: Implement purchasing](#exercise-4-implement-purchasing)
        - [Task 1: Create the transactions table](#task-1-create-the-transactions-table)
        - [Task 2: Configure the Simulator](#task-2-configure-the-simulator)
        - [Task 3: Test purchasing](#task-3-test-purchasing)
    - [Exercise 5: Implement device command and control](#exercise-5-implement-device-command-and-control)
        - [Task 1: Listen for control messages](#task-1-listen-for-control-messages)
        - [Task 2: Send control messages](#task-2-send-control-messages)
        - [Task 3: Configure the DeviceControlConsole and the Simulator](#task-3-configure-the-devicecontrolconsole-and-the-simulator)
    - [Exercise 6: Analytics with Power BI Desktop](#exercise-6-analytics-with-power-bi-desktop)
        - [Task 1: Build the query and create the visualization](#task-1-build-the-query-and-create-the-visualization)
    - [After the Hands-on Lab](#after-the-hands-on-lab)
        - [Task 1: Delete Resource Group](#task-1-delete-resource-group)

<!-- /TOC -->

# Intelligent vending machines hands-on lab unguided

## Abstract and learning objectives

In this workshop, attendees will implement an IoT solution for intelligent vending machines, leveraging facial feature recognition and Azure Machine Learning, to gain a better understanding of building cloud-based machine learning app, and real-time analytics with SQL Database in-memory and columnar indexing.

In addition, attendees will learn to:

-   Enable real-time analytics with SQL Database in-memory and columnar indexing

-   Implement distributed machine learning with R Server for HDInsight & Microsoft R Server Operationalization

-   Perform facial image processing

-   Wrangle data in Power BI Desktop

## Overview

Trey Research Inc. looks at the old way of doing things in retail and introduces innovative experiences that delight customers and drive sales. Their latest initiative focuses on intelligent vending machines that support commerce, engagement analytics, and intelligent promotions.

## Solution architecture

Below are diagrams of the solution architecture you will build in this lab. Please study this carefully, so you understand the whole of the solution as you are working on the various components.

![The preferred solution is shown to meet the customer requirements. From right to left there is an archtecture diagram which shows the connections from a mobile device to a Web Application. The Web Application is shown setting data to an Event Hub which is connected to a Web Job. From there Event Hub and Service Bus work together with Stream Analytics, Power BI and Cosomos DB to provide the full solution.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image2.png)

## Requirements

-   Microsoft Azure subscription must be pay-as-you-go or MSDN.

    -   Trial subscriptions will not work.

-   A virtual machine configured with:

    -   Visual Studio Community 2017 15.6 or later

    -   Azure SDK 2.9 or later (Included with Visual Studio 2017)

    -   [R Tools for Visual Studio](https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx) 0.3.2 or later

    -   [Power BI Desktop](http://bit.ly/2w6t2qz) (June 2016 build or later)

-   A running R Server on HD Insight Spark cluster (see [Before the Hands-on Lab](#_Before_the_Hands-on)).

## Before the hands-on lab

Duration: 75 minutes

Synopsis: In this exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all the steps provided in the Before the Hands-on Lab section to prepare your environment before attending the hackathon.

### Task 1: Provision an R Server on HDInsight with Spark cluster

Using the Azure Portal, provision a new HDInsight cluster.

1.  Open a browser and go to [https://portal.azure.com](http://bit.ly/2sUTK3q).

2.  Select **+Create a resource,** select **Data + Analytics**, **HDInsight**.

    ![Screenshot of the Azure Portal. In the left pane, Create a resource is selected. In the muddle pane, under Azure Marketplace, Data + Analytics is selected. In the right pane, under Featured, HDInsight is selected.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image3.png)

3.  On the Basics blade, enter the following settings:

    -   Cluster name: Enter a **unique name** (verified by the green checkmark).

    -   Subscription: Select the Azure subscription into which you want to deploy the cluster.

    -   Custer type: Select Configure required settings.

        ![The Azure portal is shown Cluster type Configured required settings menu.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image4.png)

        i.  On the Cluster configuration blade, set the Cluster type to **R Server** and the Version to R Server **9.1 (HDI 3.6).**

        ii. Check the box next to **R Studio community edition for R Server**. Note that the Operating System option for the Spark cluster is fixed to Linux.

        ![Screenshot of the Cluster configuration dialog box. Cluster type is set to \"R Server,\" and Version is set to \"R Server 9.1 (HDI 3.6).\" Under Features, under Available, the check box is selected for \"R Studio community edition for R Server.\"](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image5.png)

        iii. Click **Select** to close the Cluster configuration blade.

    -   Cluster login username: Leave as **admin**.

    -   Cluster login password: Enter **Password.1!!** for the admin password.

    -   Secure Shell (SSH) username: Set to **remoteuser** (this is required).

    -   Use same password as cluster login: Ensure the checkbox is checked.

    -   Resource group: Select the **Create new** radio button, and enter **ivmhandsonlab** for the resource group name.

    -   Select the desired location from the dropdown list.

    -   Select **Next** to move on to the storage settings.

        ![Screenshot of the Basics blade, with fields set to the previously mentioned settings.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image6.png "Basics blade")

4.  On the Storage blade:

    -   Primary storage type: Leave set to **Azure Storage**.

    -   Selection Method: Leave set to **My subscriptions**.

    -   Select a Storage account: Select **Create new**, and enter a name for the storage account, such as ivmlabstorage.

    -   Default container: Set to the **name of your cluster**.

    -   Additional storage accounts: Leave unconfigured.

    -   Data Lake Store access: Leave unconfigured.

    -   Metastore Settings: Leave blank.

    -   Select **Next** to move on to the Cluster summary.

        ![Screenshot of the Storage blade, with fields set to the previously mentioned settings.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image7.png "Storage blade")

5.  On the Cluster Summary blade, select Edit next to Cluster Size. 

    ![Screenshot of the Edit Cluster size link.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image8.png "Edit Cluster size link")

6.  On the Cluster size blade, enter the following:

    -   Number of worker nodes: Enter **2**.

    -   Select **Worker node size**, and select **D12 v2**, then click **Select**.

    -   Select **Head node size**, and select **D12 v2**, then click **Select**.

    -   Leave the Zookeeper node sizes set to A2.

    -   Select **R-Server edge node size**, and select **D12 v2**, then click **Select**.

    -   Select **Next**.

        ![Screenshot of the Cluster size blade, with fields set to the previously mentioned settings.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image9.png "Cluster size blade")

7.  Select **Next** on the Advanced settings blade to move to the Cluster summary blade.

8.  Select **Create** on the Cluster summary blade to create the cluster.

9.  It will take approximately 20 minutes to create you cluster. You can move on to the steps below while the cluster is provisioning.

### Task 2: Setup a lab virtual machine (VM)

1.  In the [Azure Portal](https://portal.azure.com), select **+Create a resource**, then type "Visual Studio" into the search bar. Select Visual Studio Community 2017 on Windows Server 2016 (x64) from the results. ![Screenshot of the Azure Portal, Everything pane. Under Results, under Name, Visual Studio Community 2017 (latest release) on Windows Server 2016 is circled.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image10.png "Azure Portal, Everything pane")

2.  On the blade that comes up, at the bottom, ensure the deployment model is set to Resource Manager and select Create.

    ![The Azure portal is shown Cluster type Configured required settings menu.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image11.png)

3.  Set the following configuration on the Basics tab.

    -   Name: Enter **LabVM**.

    -   VM disk type: Select **SSD**.

    -   User name: Enter **demouser**

    -   Password: Enter **Password.1!!**

    -   Subscription: Select the same subscription you used to create your cluster in [Task 1](#exercise-1-environment-setup).

    -   Resource Group: Select Use existing, and select the resource group you provisioned while creating your cluster in Task 1.

    -   Location: Select the same region you used in Task 1 while creating your cluster. 

        ![Screenshot of the Basics blade, with fields set to the previously mentioned settings.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image12.png "Create virtual machine, Basics blade")

4.  Select **OK** to move to the next step.

5.  On the Choose a size blade, ensure the Supported disk type is set to SSD, and select View all. This machine won't be doing much heavy lifting, so selecting DS2\_V3 Standard is a good baseline option. 

    ![Screenshot of the Choose a size blade. Under Supported disk type, SSD is selected. the View all button is circled, and the D2S\_V3 Standard option is circled.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image13.png "Choose a size blade")

6.  Click **Select** to move on to the Settings blade.

7.  Accept all the default values on the Settings blade, and Select **OK**.

8.  Select Create on the Create blade to provision the virtual machine. 

    ![Screenshot of the Create blade.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image14.png "Create blade")

9.  It may take 10+ minutes for the virtual machine to complete provisioning.

### Task 3: Install Power BI Desktop on the lab VM

1.  Connect to the **LabVM**. (If you are already connected to your Lab VM, skip to Step 7.)

2.  From the left side menu in the Azure portal, click on Resource groups, then enter your resource group name into the filter box, and select it from the list. 

    ![Screenshot of the Azure Portal, Resource groups pane. In the Subscriptions field, \"ivm\" is typed. under Name, \"ivmhandsonlab\" is circled.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image15.png "Azure Portal, Resource groups")

3.  Next, select your lab virtual machine, LabVM, from the list. 

    ![The LabVM has been selected in the Azure portal.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image16.png)

4.  On your Lab VM blade, select Connect from the top menu. 

    ![Screenshot of the Lab VM blade top menu, with the Connect button circled.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image17.png "Lab VM blade top menu")

5.  Download and open the RDP file.

6.  Select **Connect**, and enter the following credentials (or the non-default credentials if you changed them):

    -   User name: **demouser**

    -   Password: **Password.1!!**

7.  Once logged in, launch the **Server Manager**. This should start automatically, but you can access it via the Start menu if it does not start.

8.  Select **Local Server**, then select **On** next to **IE Enhanced Security Configuration**. 

    ![Screenshot of the Server Manager. In the left pane, Local Server is selected. In the right, Properties (For LabVM) pane, the IE Enhanced Security Configuration, which is set to On, is circled.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image18.png "Server Manager")

9.  In the Internet Explorer Enhanced Security Configuration dialog, select **Off under Administrators**, then select **OK**. 
    
    ![Screenshot of the Internet Explorer Enhanced Security Configuration dialog box, with Administrators set to Off.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image19.png "Internet Explorer Enhanced Security Configuration dialog box")

10. Close the Server Manager.

11. In a web browser on the Lab VM navigate to the Power BI Desktop download page [https://www.microsoft.com/en-us/download/details.aspx?id=45331](https://aka.ms/rtvs-current?id=45331)

12. Select the **Download Free** link in the middle of the page.

    ![The Download link has been selected for Microsoft Power BI Desktop](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image20.png)

13. Select the **x64 bit version** of the download, then click **Next**.

    ![The PBIDesktop\_x64.msi file is shown selected.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image21.png)

14. Run the installer once it downloads.

15. Select Next on the welcome screen. 

    ![Screenshot of the Power BI Installer Wizard, Welcome page.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image22.png "Power BI Installer Wizard")

16. Accept the license agreement, and select Next. 

    ![Screenshot of the Power BI Installer Wizard, Software License Terms page.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image23.png "Power BI Installer Wizard, Licence page")

17. Leave the default destination folder, and select Next. 

    ![Screenshot of the Power BI Installer Wizard, Destination Folder page.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image24.png "Power BI Installer Wizard, Destination Folder page")

18. Make sure the Create a desktop shortcut box is checked, and select Install. 

    ![Screenshot of the Power BI Installer Wizard, Ready to install page. The check box is selected for Create a desktop shortcut.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image25.png "Power BI Installer Wizard, Ready to install")

19. Uncheck Launch Microsoft Power BI Desktop, and select Finish. 

    ![Screenshot of the Power BI Installer Wizard, Setup Completed page.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image26.png "Power BI Installer Wizard, Setup Completed")

### Task 4: Prepare an SSH client

In this task, you will download, install, and prepare the Git Bash SSH client that you will use to access your HDInsight cluster from your Lab VM.

1.  On your Lab VM, open a browser, and navigate to [https://git-scm.com/downloads](https://www.microsoft.com/en-us/download/details.aspx) to download Git Bash. 

    ![Screenshot of the Git Bash Downloads webpage.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image27.png "Git Bash Downloads page")

2.  Select the download for your OS, and then select the Download button.

3.  Run the downloaded installer, select Next on each screen to accept the defaults.

4.  When the install is complete, you will be presented with the following screen: 

    ![Screenshot of the Completing the Git Setup Wizard page, with the check box selected to Launch Git Bash.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image28.png "Git Setup Wizard, Completing setup page")

5.  Check the Launch Git Bash checkbox, and uncheck View Release Notes. Select **Finish.**

6.  The Git Bash client should open in a new window.

7.  At the command prompt, enter **ssh remoteuser@\<clustername\>-ssh.azurehdinsight.net**, replacing \<clustername\> with the name of the HDInsight cluster created in [Task 1](#exercise-1-environment-setup) above.

**Note:** You will need to wait for the cluster deployment to complete in Azure before you can complete this step. ![Screenshot of the Git Bash window, with three command lines circled. The first is the remoteuser\@clustername information. The second line is a prompt asking whether you want to continue connecting. The third is the remoteuser cluster name request for password.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image29.png "Git Bash window")

8.  Respond to any prompts in the SSH window, and enter the password for **remoteuser** when prompted.

9.  Use Git Bash for SSH during the hands-on lab for any instructions requiring an SSH connection. You can repeat these steps any time to re-connect.

### Task 5: Install R Tools for Visual Studio 2017

1.  Run the Visual Studio installer using Start, Run, C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vs\_installer.exe.

    ![Visual Studio installer path is shown after clicking Start, Run.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image30.png)

2.  Once the Installer starts, click **Update**. This will take some time to complete and require a reboot.

    ![The update button has been selected from within the Visual Studio Installer.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image31.png)

3.  Once the update is complete and the VM has restarted, run the installer again. Run the Visual Studio installer using Start, Run, C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vs\_installer.exe.

4.  Next select the **Modify** option.

    ![The modify button has been selected from within the Visual Studio Installer.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image32.png)

5.  Select the Data science and analytical applications workload and then click Modify.

    ![Data science and analytical applications workload in VS2017](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image33.png)

*These steps should be completed prior to starting the rest of the Lab.*

## Exercise 1: Environment setup

Duration: 45 minutes

Trey Research has provided a starter solution for you. They have asked you to use this as the starting point for creating the Vending Machines solution in Azure.

### Task 1: Download and open the vending machines starter project

*Tasks to Complete*:

-   From your Lab VM, download the starter project from [http://bit.ly/2w6t2qz](https://git-scm.com/downloads), and unzip the contents.

-   Open VendingMachines.sln with Visual Studio 2017.

*Exit Criteria*:

-   You have a local copy of the starter project running in Visual Studio on your Lab VM.

**Note**: If you attempt to build the solution at this point, you will see many build errors. This is intentional. You will correct these in the exercises that follow.

### Task 2: Provision IoT Hub

In these steps, you will provision an instance of IoT Hub.

*Tasks to Complete*:

-   Provision an IoT Hub at the S1 Standard tier.

*Exit Criteria*:

-   You can navigate to the IoT Hub instance using the Azure Portal.

### Task 3: Create Microsoft Machine Learning Server on Linux 

In these steps, you will provision and configure a Virtual Machine running Microsoft Machine Learning Server. You will use this machine to host the R Server Operationalization service.

*Tasks to Complete*:

-   Provision a VM instance of Microsoft Machine Learning Server 9.3.0 on Ubuntu 16.04 from the Marketplace. The deployment model should be Resource Manager and size of Standard E2S V3.

-   Add an inbound bound rule to the Network Security Group attached to the Network Interface of the VM, that allows TCP connections to Port: 12800

-   Confirm there is already a default-allow-ssh rule on TCP port 22.

*Exit Criteria*:

-   You can successfully SSH into the VM.

### Task 4: Create Storage Account

In these steps, you will provision a storage account that will be used for storing photos sent from the vending machine simulator and for the storage of the promotional package resources.

*Tasks to Complete*:

-   Create a new Azure Storage account in the same region and resource group as your other resources for this hands-on lab. It can use Locally-redundant storage (LRS).

-   Using the Portal, create a container named "photos" with a private access type.

-   Using the Portal, create a container named "promos" with a private access type.

-   Using the Azure Storage explorer tool of your choice, upload the three images (CoconutWater.png, Soda.png, and Water.png) into the promo container. They can be found in the starter solution under Simulator\\Images.

*Exit Criteria*:

-   Your Azure Storage account should have a promo and photos container. The former should have the three photos from the starter solution in it.

### Task 5: Provision Cognitive Services Face API

To provision access to the Face API (which provides demographic information about photos of human subjects), you will need to provision a Cognitive Services account.

*Tasks to Complete*:

-   Provision a Cognitive Services account of type Face API.

*Exit Criteria*:

-   You can navigate to the Face API blade, and locate its key1 value, available under Settings -\> Keys.

-   You can retrieve Face API Endpoint from the Essentials block of the Overview blade.

### Task 6: Provision SQL Database

In these steps, you will provision a SQL database to support the transactions and real-time analytics.

*Tasks to Complete*:

-   Provision a SQL Database in the Premium (P1) tier.

*Exit Criteria*:

-   You can navigate to the blade for the SQL Database, and locate its ADO.NET connection string.

## Exercise 2: Create Dynamic Pricing Model

Duration: 45 minutes

In this exercise, you will create a machine learning model that predicts the purchase price for an item sold by the vending machine, provided the demographics of the customer and the item. You will then operationalize this model by exposing it as a web service hosted in Azure Machine Learning, and test it out.

### Task 1: Create a model locally

*Tasks to Complete*:

-   In Visual Studio, open TrainModel.R located within the PricingModel project.

-   Run the text between the "Create Sample Data" and "END Create Sample Data" comments, and view the sampleData created in the Variable Explorer window.

-   Complete TODO 1 through 4.

*Exit Criteria*:

-   In the same folder as your script, you should have the files sampleData.RData, pricingModel.RData, and inputExample.RData.

### Task 2: Try a prediction locally

*Tasks to Complete*:

-   In Visual Studio, open PredictUsingModel.R located within the PricingModel project.

-   Complete TODO 1 through 2.

-   Run the script.

*Exit Criteria*:

-   Using Variable Explorer, expand the prediction variable and observe the price the model suggested to use for purchasing the coconut water for the input of a 30-year-old female.
    ![Screenshot of Variable Explorer, with a purchase price suggestion of 0.949.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image34.png "Variable Explorer")

### Task 3: Create the model in R Server on HDInsight

*Tasks to Complete*:

-   SSH into your R Server on HDInsight Cluster.

-   Run the R shell.

-   Copy the script text from PredictUsingModel.r and paste it into the shell to train the model using R running across your HDInsight cluster.

-   Copy the generated files samleData.RData pricingModel.RData, and inputExample.RData to the /models folder in HDFS.

-   Use Visual Studio to download the sampleData.RData, pricingModel.RData, and inputExample.RData from Blob storage to the PricingModel project directory, overwriting existing files as necessary.

*Exit Criteria*:

-   You should have the files sampleData.RData, pricingModel.RData, and inputExample.RData copied down to the PricingModel project directory on your local machine.

### Task 4: Create predictive service in R Server Operationalization

After training a model, you want to operationalize the model so that it becomes available for integration by developers. One way to operationalize a trained model is to take the model you trained in HDInsight, and then to expose that as a predictive web service. In this task, you take a version of the scripts you have been running locally and in HDInsight and migrate them to run in the VM that is running R.

*Tasks to Complete*:

-   SSH into your Microsoft Machine Learning Server VM

-   Use the Azure CLI commands to configure Microsoft R Server operationalization with a One-box configuration.

    -   sudo -i

    -   az ml admin node setup \--onebox \--admin-password Password.1!! \--confirm-password Password.1!!

-   Update the App.config for the Simulator with the settings for:

    -   rServiceBaseAddress: with your server IP and web node port (example: http://\<your-server-ip\>:12800).

    -   rServicePassword: as set when you configured R Server operationalization.

-   Update the TODO 1 section of the PredictPricingService.r file within the PricingModelProject to reference the following packages:

    -   curl

    -   ggplot2

    -   mrsdeploy

    -   RevoScaleR

-   Update TODO 2 to remoteLogin to the R Server operationalization web node with the admin user and configured password.

-   Execute all the code interactively to publish the service.

-   Update TODO 3 to consume the API as a test, printing the result\$output("answer") value.

-   Update TODO 4 to generate the Swagger JSON file for the API, and save it as swagger.json to the project directory.

*Exit Criteria*:

-   You successfully configured Microsoft Machine Learning Server operationalization.

-   Your apiPredictPurchasePrice service was successfully published.

-   You are able retrieve a prediction result from the service.

-   You have generated the Swagger JSON file for the service.

## Exercise 3: Implement dynamic pricing

Duration: 45 minutes

In this exercise, you will implement the code that performs dynamic pricing, capitalizing on the Face API to acquire demographics, and your deployed pricing model to suggest the price based on those demographics. You will then run the vending machine simulator and see the dynamic pricing in action.

### Task 1: Implement photo uploads to Azure Storage

*Tasks to Complete*:

-   In Visual Studio Solution Explorer, expand the Simulator project and then MainWindow.xaml, and then open MainWindow.xaml.cs and scroll to the UpdateDynamicPricing method.

-   Complete TODO 1 through 3.

-   Save the file.

*Exit Criteria*:

-   You have removed any errors around TODO items 1 through 3.

### Task 2: Invoke Face API 

*Tasks to Complete*:

-   Within MainWindow.xaml.cs, locate the GetBlobSasUri method.

-   Complete TODO items 4 through 9.

-   Save the file.

*Exit Criteria*:

-   You have removed any errors around TODO items 4 through 9.

### Task 3: Invoke pricing model

*Tasks to Complete*:

-   Within MainWindow.xaml.cs, locate and complete TODO 10.

-   Save the file.

*Exit Criteria*:

-   You have removed any errors around TODO item 10.

### Task 4: Configure the Simulator

*Tasks to Complete*:

-   In Visual Studio, open App.config within the Simulator project.

-   Within the appSettings section, set the following settings:

    -   faceAPIKey: set this to the KEY 1 value for your Face API as acquired from the Azure Portal.

    -   faceEndpoint: set this to the ENDPOINT value for your Face API as acquired from the Azure Portal (for example: https://eastus2.api.cognitive.microsoft.com/face/v1.0)

    -   storageConnectionString: set this to the connection string for the Storage Account you created with the photos container.

-   Save the App.config.

*Exit Criteria*:

-   You have values for the settings in the config file.

### Task 5: Test dynamic pricing in Simulator

*Tasks to Complete*:

-   Set the Simulator project as the startup project.

-   Run the Simulator without debugging.

-   Take a picture using the simulator.

*Exit Criteria*:

-   In a few moments, you should see the price change from \$1.25 to whatever value the predictive model suggested.
    
    ![Screenshot of the Vending Machine price for coconut water, now set at \$0.91.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image35.png "Vending Machine price")

## Exercise 4: Implement purchasing

Duration: 15 minutes

In this exercise, you will create an in-memory, columnar index table in SQL DB that will be used to support purchase transactions in a real-time analytics fashion, and then implement the purchasing process in the vending machine simulator. Finally, you will run the simulator and purchase items.

### Task 1: Create the transactions table

*Tasks to Complete*:

-   Within Visual Studio Solution Explorer, expand the SQL Scripts folder and open the file Create Table.sql.

-   Complete TODO items 1 through 4.

-   Save the script.

-   Execute the script against your SQL Database.

*Exit Criteria*:

-   You have successfully executed the SQL script against the vending database.

### Task 2: Configure the Simulator

*Tasks to Complete*:

-   In the Simulator project, open App.config.

-   Within the connectionString section, set the following:

    -   TransactionsModel: set the value of the connectionString attribute to the ADO.NET connection string to your SQL DB instance.

-   Save the App.config.

*Exit Criteria*:

-   The value for the connectionString has been updated.

### Task 3: Test purchasing

*Tasks to Complete*:

-   Build the Simulator.

-   Start the Simulator without debugging.

-   In the Simulator, select Buy.

*Exit Criteria*:

-   You should see a confirmation dialog like the following:
    
    ![Screenshot of a Confirmation dialog box that says, \"Purchase complete. Enjoy!\"](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image36.png "Confirmation dialog box")

## Exercise 5: Implement device command and control

Duration: 30 minutes

In this exercise, you will implement the ability to push new promotions to the vending machine simulator using the command and control features of IoT Hub. You will update the simulator to listen for these messages. You will also update the console application DeviceControlConsole to send selected promotions.

### Task 1: Listen for control messages

*Tasks to Complete*:

-   Within Visual Studio Solution Explorer, expand the Simulator project and open the file MainWindow.xaml.cs.

-   Complete TODO items 1 through 4.

-   Save the file.

*Exit Criteria*:

-   You have no errors around TODO items 1 through 4 in MainWindow.xaml.cs.

### Task 2: Send control messages

*Tasks to Complete*:

-   Within Visual Studio Solution Explorer, expand the DeviceControlConsole project, and open the file Program.cs.

-   Complete TODO items 1 and 2.

-   Save the file.

*Exit Criteria*:

-   You have no errors around TODO items 1 and 2 in Program.cs.

### Task 3: Configure the DeviceControlConsole and the Simulator

*Tasks to Complete*:

-   In the DeviceControlConsole project, open App.config.

-   Set the IoTHubConnectionString appSetting to have a value of the connection string for the service policy to your IoT Hub. (Recall you can get this from the Azure Portal, Settings, Keys.)

-   Set the storageConnectionString appSetting to have the same connection string for your storage account that the App.config file in the Simulator project has.

-   Save the file.

-   In the Simulator project, open App.config.

-   Set the IoTHubSenderConnectionString appSetting to have a value of the connection string for the device policy to your IoT Hub (recall you can get this from the Azure Portal, Settings, Keys).

-   Set the IoTHubManagerConnectionString appSetting to have a value of the connection string for the iothubowner policy to your IoT Hub.

-   Save the file.

-   Set both the DeviceControlConsole and Simulator to startup projects for the solution.

-   Start without debugging.

*Exit Criteria*:

-   Both the Vending Machine Simulator and the DeviceControlConsole appear.
    
    ![Screenshot of the Vending Machine Simulator and DeviceControlConsole side-by-side. The vending machine simulator displays the advertisement for coconut water.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image37.png "Vending Machine Simulator and DeviceControlConsole")

-   In the DeviceControlConsole, press 1 to push the promotion for soda.

    ![Screenshot of the Vending Machine Simulator and DeviceControlConsole side-by-side. The vending machine simulator now displays the advertisement for soda.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image38.png)

-   The entire promotion surface of the vending machine changes (product name, price, and image).

## Exercise 6: Analytics with Power BI Desktop

Duration: 15 minutes

In this exercise, you will use Power BI Desktop to query purchase data from the in-memory table of SQL DB and visualize the result.

### Task 1: Build the query and create the visualization

*Tasks to Complete*:

-   Open Power BI Desktop and connect to your SQL Database.

-   Query the transactions table.

-   Create a Stacked column chart visualization that displays the sum of the purchase price by minute across all the data in the table.

*Exit Criteria*:

-   Your chart should appear as follows:
    
    ![Screenshot of the Purchase Price by Minute stacked column chart.](images/Hands-onlabunguided-Intelligentvendingmachinesimages/media/image39.png "Purchase Price by Minute")

## After the Hands-on Lab 

Duration: 10 mins

In this exercise, attendees will deprovision any Azure resources that were created in support of the lab.

### Task 1: Delete Resource Group

*Tasks to Complete*:

-   Delete the Resource Group you created.

*Exit Criteria*:

-   You have no services deployed for this hackathon remaining in your subscription.

