![](images/HeaderPic.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Intelligent vending machines
</div>

<div class="MCWHeader2">
Hands-on lab step-by-step
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

- [Intelligent vending machines hands-on lab step-by-step](#intelligent-vending-machines-hands-on-lab-step-by-step)
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
    - [After the hands-on lab](#after-the-hands-on-lab)
        - [Task 1: Delete the resource group](#task-1-delete-the-resource-group)

<!-- /TOC -->

# Intelligent vending machines hands-on lab step-by-step

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

Below are diagrams of the solution architecture you will build in this lab. Please study this carefully, so you understand the whole of the solution as you are working on the various components.![Diagram of the preferred solution. From a high-level, the commerce solution uses an API App to host the Payments web service with which the Vending Machine interacts to conduct purchase transactions. The Payment Web API invokes a 3rd party payment gateway as needed for authorizing and capturing credit card payments, and logs the purchase transaction to SQL DB. The data for these purchase transactions is stored using an In-Memory table with a Columnar Index, which will support the write-heavy workload while still allowing analytics to operate, such as queries coming from Power BI Desktop.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image2.png "Preferred high-level architecture")

## Requirements

-   Microsoft Azure subscription must be pay-as-you-go or MSDN.

    -   Trial subscriptions will not work.

-   A virtual machine configured with:

    -   Visual Studio Community 2017 15.6 or later

    -   Azure SDK 2.9 or later (Included with Visual Studio 2017)

    -   [R Tools for Visual Studio](https://aka.ms/rtvs-current) 0.3.2 or later

    -   [Power BI Desktop](https://powerbi.microsoft.com/desktop) (June 2016 build or later)

-   A running R Server on HD Insight Spark cluster (see [Before the Hands-on Lab](#_Before_the_Hands-on)).

## Before the hands-on lab

Duration: 75 minutes

Synopsis: In this exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all the steps provided in the Before the Hands-on Lab section to prepare your environment before attending the hackathon.

### Task 1: Provision an R Server on HDInsight with Spark cluster

Using the Azure Portal, provision a new HDInsight cluster.

1.  Open a browser and go to <https://portal.azure.com>.

2.  Select **+Create a resource,** select **Data + Analytics**, **HDInsight**.

    ![Screenshot of the Azure Portal. In the left pane, Create a resource is selected. In the muddle pane, under Azure Marketplace, Data + Analytics is selected. In the right pane, under Featured, HDInsight is selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image3.png)

3.  On the Basics blade, enter the following settings:

    -   Cluster name: Enter a **unique name** (verified by the green checkmark).

    -   Subscription: Select the Azure subscription into which you want to deploy the cluster.

    -   Custer type: Select Configure required settings.

        ![The Azure portal is shown Cluster type Configured required settings menu.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image4.png)

        i.  On the Cluster configuration blade, set the Cluster type to **R Server** and the Version to R Server **9.1 (HDI 3.6).**

        ii. Check the box next to **R Studio community edition for R Server**. Note that the Operating System option for the Spark cluster is fixed to Linux.

        ![Screenshot of the Cluster configuration dialog box. Cluster type is set to \"R Server,\" and Version is set to \"R Server 9.1 (HDI 3.6).\" Under Features, under Available, the check box is selected for \"R Studio community edition for R Server.\"](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image5.png)

        iii. Click **Select** to close the Cluster configuration blade.

    -   Cluster login username: Leave as **admin**.

    -   Cluster login password: Enter **Password.1!!** for the admin password.

    -   Secure Shell (SSH) username: Set to **remoteuser** (this is required).

    -   Use same password as cluster login: Ensure the checkbox is checked.

    -   Resource group: Select the **Create new** radio button, and enter **ivmhandsonlab** for the resource group name.

    -   Select the desired location from the dropdown list.

    -   Select **Next** to move on to the storage settings.

        ![Screenshot of the Basics blade, with fields set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image6.png "Basics blade")

4.  On the Storage blade:

    -   Primary storage type: Leave set to **Azure Storage**.

    -   Selection Method: Leave set to **My subscriptions**.

    -   Select a Storage account: Select **Create new**, and enter a name for the storage account, such as ivmlabstorage.

    -   Default container: Set to the **name of your cluster**.

    -   Additional storage accounts: Leave unconfigured.

    -   Data Lake Store access: Leave unconfigured.

    -   Metastore Settings: Leave blank.

    -   Select **Next** to move on to the Cluster summary.

        ![Screenshot of the Storage blade, with fields set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image7.png "Storage blade")

5.  On the Cluster Summary blade, select Edit next to Cluster Size. 

    ![Screenshot of the Edit Cluster size link.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image8.png "Edit Cluster size link")

6.  On the Cluster size blade, enter the following:

    -   Number of worker nodes: Enter **2**.

    -   Select **Worker node size**, and select **D12 v2**, then click **Select**.

    -   Select **Head node size**, and select **D12 v2**, then click **Select**.

    -   Leave the Zookeeper node sizes set to A2.

    -   Select **R-Server edge node size**, and select **D12 v2**, then click **Select**.

    -   Select **Next**.

        ![Screenshot of the Cluster size blade, with fields set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image9.png "Cluster size blade")

7.  Select **Next** on the Advanced settings blade to move to the Cluster summary blade.

8.  Select **Create** on the Cluster summary blade to create the cluster.

9.  It will take approximately 20 minutes to create you cluster. You can move on to the steps below while the cluster is provisioning.

### Task 2: Setup a lab virtual machine (VM)

1.  In the [Azure Portal](https://portal.azure.com/), select **+Create a resource**, then type "Visual Studio" into the search bar. Select Visual Studio Community 2017 on Windows Server 2016 (x64) from the results. 

    ![Screenshot of the Azure Portal, Everything pane. Under Results, under Name, Visual Studio Community 2017 (latest release) on Windows Server 2016 is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image10.png "Azure Portal, Everything pane")

2.  On the blade that comes up, at the bottom, ensure the deployment model is set to Resource Manager and select Create.

    ![The Azure portal is shown with a Resource Manager deployment model.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image11.png)

3.  Set the following configuration on the Basics tab.

    -   Name: Enter **LabVM**.

    -   VM disk type: Select **SSD**.

    -   User name: Enter **demouser**

    -   Password: Enter **Password.1!!**

    -   Subscription: Select the same subscription you used to create your cluster in [Task 1](#task-1-provision-an-r-server-on-hdinsight-with-spark-cluster).

    -   Resource Group: Select Use existing, and select the resource group you provisioned while creating your cluster in Task 1.

    -   Location: Select the same region you used in Task 1 while creating your cluster. 

        ![Screenshot of the Basics blade, with fields set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image12.png "Create virtual machine, Basics blade")

4.  Select **OK** to move to the next step.

5.  On the Choose a size blade, ensure the Supported disk type is set to SSD, and select View all. This machine won't be doing much heavy lifting, so selecting DS2\_V3 Standard is a good baseline option. 

    ![Screenshot of the Choose a size blade. Under Supported disk type, SSD is selected. the View all button is circled, and the D2S\_V3 Standard option is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image13.png "Choose a size blade")

6.  Click **Select** to move on to the Settings blade.

7.  Accept all the default values on the Settings blade, and Select **OK**.

8.  Select Create on the Create blade to provision the virtual machine. 

    ![Screenshot of the Create blade.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image14.png "Create blade")

9.  It may take 10+ minutes for the virtual machine to complete provisioning.

### Task 3: Install Power BI Desktop on the lab VM

1.  Connect to the **LabVM**. (If you are already connected to your Lab VM, skip to Step 7.)

2.  From the left side menu in the Azure portal, click on Resource groups, then enter your resource group name into the filter box, and select it from the list. 

    ![Screenshot of the Azure Portal, Resource groups pane. In the Subscriptions field, \"ivm\" is typed. under Name, \"ivmhandsonlab\" is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image15.png "Azure Portal, Resource groups")

3.  Next, select your lab virtual machine, LabVM, from the list. 

    ![The LabVM has been selected in the Azure portal.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image16.png)

4.  On your Lab VM blade, select Connect from the top menu. 

    ![Screenshot of the Lab VM blade top menu, with the Connect button circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image17.png "Lab VM blade top menu")

5.  Download and open the RDP file.

6.  Select **Connect**, and enter the following credentials (or the non-default credentials if you changed them):

    a.  User name: **demouser**

    b.  Password: **Password.1!!**

7.  Once logged in, launch the **Server Manager**. This should start automatically, but you can access it via the Start menu if it does not start.

8.  Select **Local Server**, then select **On** next to **IE Enhanced Security Configuration**. 

    ![Screenshot of the Server Manager. In the left pane, Local Server is selected. In the right, Properties (For LabVM) pane, the IE Enhanced Security Configuration, which is set to On, is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image18.png "Server Manager")

9.  In the Internet Explorer Enhanced Security Configuration dialog, select **Off under Administrators**, then select **OK**. 

    ![Screenshot of the Internet Explorer Enhanced Security Configuration dialog box, with Administrators set to Off.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image19.png "Internet Explorer Enhanced Security Configuration dialog box")

10. Close the Server Manager.

11. In a web browser on the Lab VM navigate to the Power BI Desktop download page <https://www.microsoft.com/en-us/download/details.aspx?id=45331>

12. Select the **Download Free** link in the middle of the page.

    ![The Download link has been selected for Microsoft Power BI Desktop](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image20.png)

13. Select the **x64 bit version** of the download, then click **Next**.

    ![The PBIDesktop\_x64.msi file is shown selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image21.png)

14. Run the installer once it downloads.

15. Select Next on the welcome screen. 

    ![Screenshot of the Power BI Installer Wizard, Welcome page.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image22.png "Power BI Installer Wizard")

16. Accept the license agreement, and select Next. 

    ![Screenshot of the Power BI Installer Wizard, Software License Terms page.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image23.png "Power BI Installer Wizard, Licence page")

17. Leave the default destination folder, and select Next. 

    ![Screenshot of the Power BI Installer Wizard, Destination Folder page.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image24.png "Power BI Installer Wizard, Destination Folder page")

18. Make sure the Create a desktop shortcut box is checked, and select Install. 

    ![Screenshot of the Power BI Installer Wizard, Ready to install page. The check box is selected for Create a desktop shortcut.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image25.png "Power BI Installer Wizard, Ready to install")

19. Uncheck Launch Microsoft Power BI Desktop, and select Finish. 

    ![Screenshot of the Power BI Installer Wizard, Setup Completed page.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image26.png "Power BI Installer Wizard, Setup Completed")

### Task 4: Prepare an SSH client

In this task, you will download, install, and prepare the Git Bash SSH client that you will use to access your HDInsight cluster from your Lab VM.

1.  On your Lab VM, open a browser, and navigate to <https://git-scm.com/downloads> to download Git Bash. 

    ![Screenshot of the Git Bash Downloads webpage.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image27.png "Git Bash Downloads page")

2.  Select the download for your OS, and then select the Download button.

3.  Run the downloaded installer, select Next on each screen to accept the defaults.

4.  When the install is complete, you will be presented with the following screen: 

    ![Screenshot of the Completing the Git Setup Wizard page, with the check box selected to Launch Git Bash.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image28.png "Git Setup Wizard, Completing setup page")

5.  Check the Launch Git Bash checkbox, and uncheck View Release Notes. Select **Finish.**

6.  The Git Bash client should open in a new window.

7.  At the command prompt, enter **ssh remoteuser@\<clustername\>-ssh.azurehdinsight.net**, replacing \<clustername\> with the name of the HDInsight cluster created in [Task 1](#task-1-provision-an-r-server-on-hdinsight-with-spark-cluster) above.

**Note:** You will need to wait for the cluster deployment to complete in Azure before you can complete this step. 

![Screenshot of the Git Bash window, with three command lines circled. The first is the remoteuser\@clustername information. The second line is a prompt asking whether you want to continue connecting. The third is the remoteuser cluster name request for password.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image29.png "Git Bash window")

8.  Respond to any prompts in the SSH window, and enter the password for **remoteuser** when prompted.

9.  Use Git Bash for SSH during the hands-on lab for any instructions requiring an SSH connection. You can repeat these steps any time to re-connect.

### Task 5: Install R Tools for Visual Studio 2017

1.  Run the Visual Studio installer using Start, Run, C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vs\_installer.exe.

    ![Visual Studio installer path is shown after clicking Start, Run.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image30.png)

2.  Once the Installer starts, click **Update**. This will take some time to complete and require a reboot.

    ![The update button has been selected from within the Visual Studio Installer.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image31.png)

3.  Once the update is complete and the VM has restarted, run the installer again. Run the Visual Studio installer using Start, Run, C:\\Program Files (x86)\\Microsoft Visual Studio\\Installer\\vs\_installer.exe.

4.  Next select the **Modify** option.

    ![The modify button has been selected from within the Visual Studio Installer.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image32.png)

5.  Select the Data science and analytical applications workload and then click Modify.

    ![Data science and analytical applications workload in VS2017](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image33.png)

    *These steps should be completed prior to starting the rest of the Lab.*

## Exercise 1: Environment setup

Duration: 45 minutes

Trey Research has provided a starter solution for you. They have asked you to use this as the starting point for creating the Vending Machines solution in Azure.

### Task 1: Download and open the vending machines starter project

1.  From your LabVM, download the starter project from the following URL: <http://bit.ly/2w6t2qz>

2.  Unzip the contents to the folder **C:\\VendingMachines\\.**

3.  Open **VendingMachines.sln** with Visual Studio 2017.

4.  Sign in to Visual Studio or create an account, if prompted.

5.  If the Security Warning for Simulator window appears, **uncheck Ask me for every project in this solution**, and select **OK**. 

    ![Screenshot of the Security warning for Simulator window. The checkbox is cleared for \"Ask me for every project in this solution.\"](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image34.png "Security warning for Simulator")

**Note**: If you attempt to build the solution at this point, you will see many build errors. This is intentional. You will correct these in the exercises that follow.

### Task 2: Provision IoT Hub

In these steps, you will provision an instance of IoT Hub.

1.  In your browser, navigate to the Azure Portal (<https://portal.azure.com)>.

2.  Select **+Create a resource**, then select Internet of Things, and select IoT Hub.

    ![Screenshot of the Azure Portal. In the left pane, New is selected. In the center pane, under Azure Marketplace, Internet of Things is selected. In the right pane, under Featured, IoT Hub is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image35.png)

3.  In the IoT Hub blade, enter the following:

    -   Name: Provide a name for your new IoT Hub, such as **vendingmachingshub**

    -   Pricing and scale tier: **Select S1 Standard**

    -   IoT Hub units: **Set to 1**

    -   Device-to-cloud partitions: **Select 4 partitions.**

    -   Subscription: Select the same subscription you've been using for previous resources in this lab.

    -   Resource group: Select Use existing, and select the **ivmhandsonlab** resource group you created previously.

    -   Location: Select the location you used previously.

    -   Select **Create**.

        ![Screenshot of the IoT Hub blade, with fields set to the previously mentioned values.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image36.png "IoT Hub blade")

4.  When the IoT Hub deployment is completed, you will receive a notification in the Azure portal. Select **Go** to resource in the notification.

    ![Screenshot of the Deployment succeeded message, with the Go to resource button circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image37.png "Deployment succeeded message")

5.  From the IoT Hub's Overview blade, select **Shared access policies** under Settings on the left-hand menu. 

    ![Screenshot of the Overview blade, settings section. Under Settings, Shared access policies is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image38.png "Overview blade, settings section")

6.  Select **iothubowner** policy.

    ![The Azure portal is shown with the iothubowner selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image39.png)

7.  In the **iothubowner** blade, select the Copy button to the right of the Connection string - primary key field. Paste the connection string value into a text editor, such as Notepad, as this will be needed later in this lab. 

    ![Screenshot of the iothubowner blade. A callout points to the copy button to the right of the connection string - primary key field.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image40.png "iothubowner blade")

### Task 3: Create Microsoft Machine Learning Server on Linux 

In these steps, you will provision and configure a Virtual Machine running Microsoft Machine Learning Server. You will use this machine to host the R Server Operationalization service.

1.  Within the Azure Portal, select **+Create a resource**, then type **Machine Learning Server** into the Search field.

2.  In the results list, select **Microsoft Machine Learning Server 9.3.0 on Ubuntu 16.04**.

    ![Screenshot of hte Azure Portal, Everything pane. In the Search field, Machine learning server is typed. Under Results, under Name, \"Microsoft Machine Learning Server 9.3.0 on Ubuntu 16.04\" is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image41.png)

3.  On the blade that appears, select **Create.**

4.  In the Basics blade, enter:

    -   Name: Enter a **unique name** for the server.

    -   User name: Enter **radmin**.

    -   Authentication type: Select **Password**.

    -   Password: Enter and confirm the password, **Password.1!!**

    -   Subscription: Select the subscription you've been using for this lab.

    -   Resource group: Select **Use existing**, and select the Resource Group you created earlier.

    -   Location: Select the same location used previously.

    -   Select OK to move on to choosing a VM size. 

        ![Screenshot of the Basics blade, with fields set to the previously mentioned values.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image42.png "Basics blade")

5.  On the Choose a size blade, select **E2S\_V3 Standard**, and click **Select**. 

    ![Screenshot of the Choose a size blade, with E2S\_V3 Standard circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image43.png "Choose a size blade")

6.  On the Settings blade, select **Network security group** (firewall).

    -   Click **Create new** under Choose network security group.

    -   Enter a name, such as **my-r-nsg**.

    -   Select **+Add** **an inbound rule**. 
    
    ![Screenshot of the Settings, Choose network security group, and Create network security group blades. In the settings blade, Network security group (firewall) is selected. In the Choose network security group blade, Create new is selected. In the Create network security group blade, + Add an inbound rule is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image44.png "Settings, Choose network security group, and Create network security group Blades")

    -   Select **Custom** under Service and enter the following:

        i.  Port range: **12800**

        ii. Priority: **100**

        iii. Name: **Port\_12800** (should be auto-generated)

        iv. Select **OK**

        ![Screenshot of the Add inbound security rule blade, with the Port ranges field circled and set to 12800.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image45.png "Add inbound security rule blade")

7.  Select **OK** on the Create network security group blade.

8.  Select **OK** on the Settings blade.

9.  Select Create on the Summary blade. 

    ![Screenshot of the summary blade.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image46.png "Summary blade")

10. Once the machine has provisioned you will need to perform some configurations. On top of the Overview blade of the new server, select Connect, then copy the SSH command.
    
    ![Screenshot of the Overview blade top menu, which has Connect circled. In the section below, the ssh command ssh radmin\@52.168.132.221 is lighlighted.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image47.png "Overview blade")

11. Using a new Git Bash window on your Lab VM, SSH into your Microsoft Machine Learning Server VM by pasting the SSH command you copied above at the command prompt. For example:\
    **ssh radmin@\<your-server-ip\>.**

12. When prompted if you want to continue connecting, enter **yes**.

13. Enter your password, **Password.1!!**

14. At the prompt, after successfully logging in, enter the following command: **sudo apt-get update -y**

15. Type **exit** twice to disconnect from the ssh session.

### Task 4: Create Storage Account

In these steps, you will provision a storage account that will be used for storing photos sent from the vending machine simulator and for the storage of the promotional package resources.

1.  Using the Azure Portal, select **+Create a resource**, **Storage**, then select **Storage account**.

    ![Screenshot of the Azure Portal. In the left pane, New is selected. In the middle pane, under Azure Marketplace, Storage is selected. In the right pane, under Featured, Storage account - blob, file, table, queue is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image48.png)

2.  On the Create storage account blade, enter the following values:

    -   Name: Enter a **unique name** for the storage account.

    -   Deployment model: Leave **Resource Manager** selected.

    -   Account kind: Leave set at **General purpose**.

    -   Performance: Leave set to **Standard**.

    -   Replication: Set to **Locally-redundant storage (LRS).**

    -   Storage service encryption: Leave as **Disabled**.

    -   Subscription: Select your Subscription.

    -   Resource group: Select the Use existing radio button, and select **ivmhandsonlab** from the resource group list.

    -   Location: Select a Location to be consistent with the other resources you have created.

    -   Virtual networks : Leave set to **Disabled.**

    -   Select Create.
        
        ![Screenshot of the Create storage account blade, with field values set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image49.png "Create storage account blade")

3.  Navigate to the newly created storage account in the Azure Portal by clicking on Storage Accounts, and selecting it from the list of available storage accounts.

4.  On the Storage account blade, select Blobs.
    
    ![Screenshot of the Storage account blade, services section. Under Services, Blobs is selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image50.png "Storage account blade, services section")

5.  In the Blob service blade, select **+Container** from the command bar.

    ![Screenshot of the Blob service blade command bar, with Container circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image51.png "Blob service blade command bar")

6.  On the New container blade, set the name to "**photos**" and select **Private** as the Access type.
    
    ![Screenshot of the New container blade Name and Public access level fields.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image52.png "New container blade")

7.  Click **OK**.

8.  Repeat steps 6-8 to create another container named "**promo**".

9.  You should now see both containers listed on the Blob service blade. 

    ![Screenshot of the Blob service blade name rows, with photos and promo listed.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image53.png "Blob service blade")

10. Close the Blob service blade to go back to the Storage blade. Select **Access Keys** from the left-hand menu.

    ![Screenshot of the Settings section on the Storage blade. Access keys is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image54.png "Storage blade, Settings section")

11. Use the copy button to the right of the Connection String for key1 to copy your storage connection string. Save the copied value to a text editor, such as Notepad, as this will be used later on.
    
    ![The keys for the storage account are shown in the Azure portal. The Connection string has been copied.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image55.png)

12. Open Visual Studio and from the **View Menu** select **Cloud Explorer**.
    
    ![Screenshot of the Visual Studio View menu, with Cloud Explorer selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image56.png "Visual Studio View menu")

**Note:** You may need to select your subscription, but clicking the person icon and expanding the subscriptions.

13. Expand the **Storage account** that you just created, and the **Blob Containers** item underneath it.
    
    ![Screenshot of the Visual Studio Tree view, which is expanded to: Storage Accounts\\vendinghackphotos\\Blob Containers.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image57.png "Visual Studio Tree view")

14. Right-click the promo container and select Open.
    
    ![Screenshot of the Visual Studio Tree view. The vendinghackphotos container is selected, with Open selected in the sub-menu below.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image58.png "Visual Studio Tree view")

15. Select the Upload blob button.
    
    ![Screenshot of the Upload blob button.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image59.png "Upload blob button")

16. Select **Browse**.

17. In the dialog, select the three images **CoconutWater.png, Water.png, and Soda.png** from the starter solution **Simulator\\Images** folder and select **Open**.

18. Select **OK** on the Upload New File Dialog to upload the images to the container.
    
    ![Screenshot of the Updated New File window.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image60.png "Updated New File window")

### Task 5: Provision Cognitive Services Face API

To provision access to the Face API (which provides demographic information about photos of human subjects), you will need to provision a Cognitive Services account.

1.  In the Azure Portal, select **+Create a resource, AI + Cognitive Services,** and select **Face API**.

    ![Screenshot of the Azure Portal. In the left pane, New is selected. In the New pane, on the left, AI = Cognitive Services is selected, and on the right, Face API is selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image61.png)

2.  On the Create Face API blade:

    -   Name: Enter a name.

    -   Subscription: Choose your Subscription.

    -   Location: Choose the location you've been using for other resources in this lab.

    -   Pricing tier: Select the **Free tier (F0)** from the drop-down list.

    -   Resource group: Select Use existing, and select the **ivmhandsonlab** resource group from the list.

    -   Check the box confirming you have read and understand the legal terms.

    -   Click Create to provision the Cognitive Services account.

        ![Screenshot of the Create blade, with field values set to previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image62.png "Create blade")

3.  When the Face API finishes provisioning, browse to the Cognitive Services Face API by clicking on Go to resource in the Deployment succeeded notification.

    ![Screenshot of the Deployment succeeded message, with the Go to resource button circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image63.png "Deployment succeeded message")

4.  On top of the Cognitive Services overview blade, click the Copy button to the right of the Endpoint. Paste this value into a text editor, such as Notepad, for later use. 

    ![Screenshot of the Cognitive Services overview blade, Essentials section. The Endpoint url is circled, and a callout points to the \"Click to Copy\" button.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image64.png "Cognitive Services overview blade, Essentials section")

5.  In the Cognitive Services blade, click on Keys under the Resource Management heading. 

    ![Screenshot of the Keys button.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image65.png "Keys button")

6.  Click the Copy button next to the value for Key 1. Paste this value into a text editor, such as Notepad, for later use.
    
    ![The Azure portal is shown with Key copied for the API.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image66.png)

### Task 6: Provision SQL Database

In these steps, you will provision a SQL database to support the transactions and real-time analytics.

1.  In the Azure Portal, select **+Create a resource**, select **Databases**, then select **SQL Database**.

    ![Screenshot of the Azure Portal, New pane. In the left pane, New is selected. In the right, New pane, under Azure Marketplace, Databases is selected. On the right, under Featured, SQL Database (Quickstart tutorial) is selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image67.png)

2.  In the SQL Database blade, enter the following:

    -   Database name: Enter **vending**.

    -   Subscription: Choose your Subscription.

    -   Resource Group: Select Use existing, and choose **ivmhandsonlab** from the resource group list.

    -   Source: Leave source as **Blank** database.

    -   Server: Select **configure required settings**

        i.  Server name: Enter a unique server name

        ii. User name: Enter **demouser**

        iii. Password: Enter **Password.1!!**

        iv. Location: Select the same location you've used for other resources in this lab.

        v.  Click Select.

        ![Screenshot of the SQL Database blade, with field values set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image68.png "SQL Database blade")

    -   Want to use SQL elastic pool: Leave set to Not now.

    -   Change the Pricing tier to **Premium P1**, and select Apply. Premium tier is required for Columnar indexes.
        
        ![Screenshot of the Configure performance blade, with the Premium pricing tier selected, and the monthly cost at \$465.00.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image69.png "Configure performance blade")

    -   Collation: Leave set to the default value (SQL\_Latin1\_General\_CP1\_CI\_AS).

    -   Select **Create**.
        
        ![Screenshot of the SQL Database blade, with field values set to the previously mentioned settings.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image70.png "SQL Database")

3.  Once the SQL Database finishes provisioning, navigate to the database in the Azure portal and select the Show database connection strings near the top of the Overview blade. 

    ![Screenshot of the Azure Portal, Database section. Under Connection strings, the link to \"Show database connection strings\" is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image71.png "Azure Portal, Database section")

4.  Copy the connection string on the ADO.NET tab of the Database connection string blade, and paste the value into a text editor, such as Notepad, for later reference. 
    
    ![Screenshot of the ADO.NET tab, with the connection string selected, and the copy button circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image72.png "ADO.NET tab")

## Exercise 2: Create Dynamic Pricing Model

Duration: 45 minutes

In this exercise, you will create a machine learning model that predicts the purchase price for an item sold by the vending machine, provided the demographics of the customer and the item. You will then operationalize this model by exposing it as a web service hosted in Azure Machine Learning, and test it out.

### Task 1: Create a model locally

1.  Within Visual Studio Solution Explorer, expand the **PricingModel** project and open the file **TrainModel.R** by double-clicking on the file in the Solution Explorer.

    ![Screenshot of the Visual Studio Solution Explorer, expanded to: Solution \'VendingMachines\'\\PricingModel\\TrainModel.R.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image73.png "Visual Studio Solution Explorer")

2.  Read the script. The top portion, entitled Create Sample Data, has been provided for you and you will generate the sample data you will use to train your model.

3.  Highlight all the text between the "Create Sample Data" and "END Create Sample Data" comments.

4.  Right-click the selected text and select Execute In Interactive. 
    
    ![Screenshot of the Train Model tab, with Execute In Interactive circled on the sub-menu.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image74.png "Train Model tab")

5.  You should see it execute in the R Interactive Window, ending with a summary of the created data. 
 
    ![Screenshot of the R Interactive - Microsoft R Client window. ](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image75.png "R Interactive - Microsoft R Client window")

6.  From the R Tools menu, select Windows and Variable Explorer.
    
    ![Screenshot of the R tools menu, with Windows / Variable Explorer selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image76.png "R tools menu")

7.  Expand the variable **sampleData**, and explore the structure of the created data.
    
    ![Screenshot of the Variable Explorer, with sampleData selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image77.png "Variable Explorer")

8.  Now save this sampleData to a file by replacing TODO 1 in the TrainModel.R script with the following code:
    ```
    # TODO: 1. Export the sample data to a file   
    save(sampleData, file = "sampleData.RData")
    ```

9.  Highlight the **save line**, and select **Execute In Interactive**.

10. Open File Explorer and navigate to the location of the **PricingModel** (C:\\VendingMachines\\VendingMachines - Clean\\PricingModel) project on disk. **You should see** the file **sampleData.RData** on disk.

    ![Screenshot of File Explorer, with sampleData.RData selected, and the pop-up listing the Type (R Workspace), Size (2.85 KB), and Data/time modified.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image78.png "File Explorer, sampleData.RData")

11. Back in the **TrainModel.R** file in Visual Studio, replace TODO 2 with the following code that builds the model using a Linear Regression.
    ```
    # TODO: 2. Build a linear regression model to predict purchase price given age, gender and # productSelect 
    pricingModel <- rxLinMod(purchasePrice ~ age + gender + productSelected, data = sampleData)
    ```

12. Save that trained model to disk by replacing TODO 3 with:
    ```
    # TODO: 3. Export the trained model to a file named pricingModel.rda 
    save(pricingModel, file = "pricingModel.RData")
    ```

13. Finally, save the first row of the sample data to a file so you can re-use the structure later when operationalizing the model. Replace TODO 4 with:
    ```
    # TODO: 4. Save one example of the sample data to serve as an input template, to a file     # called inputExample.rda
    inputExample <- sampleData[1,]
    save(inputExample, file = "inputExample.RData")
    ```

14. Save your changes to **TrainModel.R**.

15. Highlight TODO items 2 through 4 and execute them in interactive.

16. In the same folder as your script, you should now have the files **sampleData.RData**, **pricingModel.RData**, and **inputExample.RData**. 

    ![Screenshot of File Explorer, with the previously mentioned files circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image79.png "File Explorer")

### Task 2: Try a prediction locally

1.  Within Visual Studio, open **PredictUsingModel.r** under the Pricing Model project in Solution Explorer.

2.  Replace TODO 1 with the following:
    ```
    # TODO: 1. Prepare the input to use for prediction
    inputExample[1,]$age <- 30
    inputExample[1,]$gender <- "F"
    inputExample[1,]$productSelected <- "coconut water"
    ```

3.  Replace TODO 2 with the following:
    ```
    # TODO: 2. Execute the prediction
    prediction <- rxPredict(pricingModel, data = inputExample)
    ```

4.  Highlight all the script in the file and execute it in interactive.

5.  Using Variable Explorer, expand the prediction variable and observe the price the model suggested to use for purchasing the coconut water for input of a 30-year-old female.
    
    ![Screenshot of Variable Explorer, prediction table. The purchasePrice\_Prec is 0.949.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image80.png "Variable Explorer")

### Task 3: Create the model in R Server on HDInsight

1.  On your LabVM, open a Git Bash shell like you did in the [Before the Hands-on Lab, Task 4](#task-4-prepare-an-ssh-client), Step 7.

2.  SSH into your deployed R Server in HDInsight cluster. (You can get the SSH connection string for your cluster from the HDInsight Blade in the Azure Portal). 

    ![Screenshot of the HDInsight cluster blade for vendingmachineslab. Overview is selected in the left pane, and in the right pane, on the menu bar, Secure Shell (SSH) is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image81.png "HDInsight cluster blade")![Screenshot of Connect to cluster. Ssh.azurehdinsight.net is circled in the Hostname field, with the endpoint circled below.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image82.png "Connect to cluster section")

3.  If prompted to continue connecting, enter yes.

4.  Enter your password.

5.  At the command prompt, type **R** to load the R shell (be sure to use a capital letter "R").

6.  Run the following command to create a spark context for R:
    ```
    sparkCluster <- RxSpark()
    rxSetComputeContext(sparkCluster)
    ```

7.  In Visual Studio, open **TrainModel.r**, and copy the entire script.

8.  Paste the script in the R shell, and press ENTER. (You may need to press ENTER a few times until you get to the last line of the script.)

9.  When the script has finished executing, type the following:
    ```
    dir()
    ```

10. You should see it list the three files created by the script, as follows:\
    ![Screenshot of a Visual Studio window listing the following three files: inputExample.RData, pricingModel.RData, and sampleData.RData.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image83.png "Visual Studio ")

11. Now, copy those files from local storage to Blob storage by using the Hadoop File System. First, create a folder in which to store your output.
    ```
    modelExportDir <- "/models"
    rxHadoopMakeDir(modelExportDir)
    ```

12. List the contents of the root ("/") directory, and confirm your "**/models**" folder has been created. Notice that the list you are looking at is folders directly underneath the container in Azure Storage that was created with your cluster.
    ```
    rxHadoopListFiles("/")
    ```

    ![Screenshot of the Visual Studio window, with the previously mentioned information displaying, and the /models folder information circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image84.png "Visual Studio window")

13. Copy the **pricingModel.RData** from the local directory to the **/models** folder in HDFS by running the following command:
    ```
    rxHadoopCopyFromLocal("pricingModel.RData", modelExportDir)
    ```

14. Repeat the previous step for inputExample.RData and sampleData.RData.

15. Run the following command to verify the three files now exist in HDFS (and Blob storage), under /models.
    ```
    rxHadoopListFiles("/models")
    ```

16. The output should look similar to the following:
    
    ![Screenshot of the Visual Studio window with output verifying that the three files exist in HDFS.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image85.png "Visual Studio window")

17. Using Visual Studio, Cloud Explorer, navigate to the storage account for your HDInsight cluster, expand: 

    ![Screenshot of Visual Studio, Cloud Explorer expanded as: ivmlabstorage\\Blob Containers\\vendingmachineslab.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image86.png "Visual Studio, Cloud Explorer")

18. Right-click the storage container, and select **Open.**

19. In the editor that appears, double-click the models folder, and verify you see your files.
    
    ![Screenshot of the Editor, hackathoncluster2(Container) tab. Under models/, three files display: inputExampleRData, pricingModelRData, and sampleDataRData.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image87.png "Editor, hackathoncluster2(Container) tab")

20. Right-click **inputExample.RData** and select Save As... and choose the directory for your **PricingModel** project, overwriting files if prompted.

21. Repeat the previous step for **pricingModel.RData** and **sampleData.RData**.

22. You have now used R Server on HDInsight to train a model that you can then upload to R Server Operationalization to expose it as a web service.

### Task 4: Create predictive service in R Server Operationalization

After training a model, you want to operationalize the model so that it becomes available for integration by developers. One way to operationalize a trained model is to take the model you trained in HDInsight, and then to expose that as a predictive web service. In this task, you take a version of the scripts you have been running locally and in HDInsight and migrate them to run in the VM that is running R.

16. In the Azure portal, navigate to the Microsoft Machine Learning Server Virtual Machine you created in [Exercise 1, Task 3](#task-3-create-microsoft-machine-learning-server-on-linux).

17. On top of the Overview blade, select Connect, then copy the SSH command.
    
    ![Screenshot of the Overview blade top menu, which has Connect circled. In the section below, the ssh command ssh radmin\@52.168.132.221 is lighlighted.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image47.png "Overview blade")

18. Using a new Git Bash window on your Lab VM, SSH into your Microsoft Machine Learning Server VM by pasting the SSH command you copied above at the command prompt. For example:
    **ssh radmin@\<your-server-ip\>.**

19. When prompted if you want to continue connecting, enter yes.

20. Enter your password, **Password.1!!**

21. At the prompt, after successfully logging in, you will need to complete a few tasks to configure and operationalize the environment.

22. Run the following command to act as root: **sudo -i**

23. Now that you are acting as root run the following command to: **az ml admin node setup \--onebox \--admin-password Password.1!! \--confirm-password Password.1!!**

    ![Command to setup ML server to host a webservice has been completed. the port 12800 is highlighted.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image88.png)

24. In Visual Studio, open the **App.config** for the Simulator project. This can be done by expanding the Simulator project in the Solution Explorer, and double-clicking on **App.config** under the **Simulator** project. 

    ![The Visual Studio Solution Explorer has Simulator highlighted and expanded, and under it, App.config circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image89.png "Visual Studio Solution Explorer")

25. Locate the **appSetting** **rServiceBaseAddress** and enter **http://\<your-server-public-ip\>:12800** for the value. (For example: <http://52.168.132.221:12800>). Your server IP address is the same IP address you used for the SSH connection in Step 3 of this task, above.

26. Locate the **appSetting** **rServicePassword** and update its value with the password you defined when you configured your R server for Operationalization (step 11 above), **Password.1!!** 

    ![The App.config file for the project is open with the AppSettings for three keys shown. These include the URL to the server, user name and password.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image90.png)

27. Save **App.config**.

28. Open **PredictPricingService.r** within the **PricingModel** project in the Visual Studio Solution Explorer.

    ![The Visual Studio Solution Explorer has PricingModel expanded, and under it, PredictPricingService.r circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image91.png "Visual Studio Solution Explorer")

29. Find TODO 1, and replace with the following code block:
    ```
    # TODO: 1. Load packages needed for operationalization
    usePackage <- function(p) {
    if (!is.element(p, installed.packages()[, 1]))
    install.packages(p, dep = TRUE)
    library(p, character.only = TRUE)
    }
    usePackage("curl")
    usePackage("ggplot2")
    usePackage("mrsdeploy")
    usePackage("RevoScaleR")
    ```

30. Find TODO 2, and replace with the following code block to remotely connect to the R Server Operationalization service:
    ```
    # TODO: 2. Configure remote login
    remoteLogin(
    deployr_endpoint = "http://<your-server-ip>:12800",
    username = "admin",
    password = "<your-admin-password>"
    )
    pause()
    ```

**Note:** Make sure to replace **\<your-server-ip\>** with the IP address with your VM's IP address, and enter the password you specified when you configured your R server for Operationalization (step 11) in place of **\<your-admin-password\>**. The TODO 2 section should looking something like the following screen shot.

![Screenshot of the TODO 2 section, with the IP address and password circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image92.png "TODO 2 section")

31. Highlight all the code in **PredictPricingService.r**, right-click and then **execute in interactive**. The last output status in the R Interactive window should be "Published service".

32. Find TODO 3 and replace with the following code block to consume the API as a test:
    ```
    # TODO: 3. Consume the API as a test
    services <- listServices("apiPredictPurchasePrice")
    serviceName <- services[[1]]
    api <- getService(serviceName$name, serviceName$version)
    result <- api$apiPredictPurchasePrice(30, "F", "coconut water")
    print("Result: ")
    print(result$output("answer"))
    result
    ```

33. Find TODO 4 and replace with the following code block to generate and save the Swagger JSON file for the API:
    ```
    # TODO: 4. Generate the Swagger JSON file for the API
    swagger <- api$swagger()
    cat(swagger, file = "swagger.json", append = FALSE)
    ```

34. Highlight just the TODO 3 and TODO 4 code blocks you added and execute in interactive.

35. When you scroll up through the R Interactive window results, you should see an output with your prediction like the following:
    ```
    $success
    [1] TRUE

    $errorMessage
    [1] ""

    $outputParameters
    $outputParameters$purchasePrice
    [1] 0.9348741
    ```

36. Also open the swagger.json file in your PricingModel project directory to view its contents. This file can be used within the swagger.io online editor to generate client code to connect to your service. We have already done this for you within the included IO.Swagger project.

## Exercise 3: Implement dynamic pricing

Duration: 45 minutes

In this exercise, you will implement the code that performs dynamic pricing, capitalizing on the Face API to acquire demographics, and your deployed pricing model to suggest the price based on those demographics. You will then run the vending machine simulator and see the dynamic pricing in action.

### Task 1: Implement photo uploads to Azure Storage

1.  In Visual Studio Solution Explorer, expand the Simulator project and then **MainWindow.xaml** and then open **MainWindow.xaml.cs**.

    ![Visual Studio Solution Explorer is expanded as: Simulator\\MainWindow.xaml\\MainWindow.xaml.cs.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image93.png "Visual Studio Solution Explorer")

2.  Scroll down to the method **UpdateDynamicPricing**. 

    ![The UpdateDynamicPrice function is high-lighted in Visual Studio.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image94.png)

3.  Replace TODO 1 with the following:
    ```
    // TODO 1. Retrieve storage account from connection string.
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
    CloudBlobContainer container = blobClient.GetContainerReference("photos");
    ```

4.  Replace TODO 2 with the following:
    ```
    // TODO 2. Retrieve reference to a blob named with the value of fileName.
    string blobName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(filename);
    CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
    ```

5.  Replace TODO 3 with the following:
    ```
    // TODO 3. Create or overwrite the blob with contents from a local file.
    using (var fileStream = System.IO.File.OpenRead(filename))
    {
    blockBlob.UploadFromStream(fileStream);
    }
    ```

6.  Save MainWindow.xaml.cs.

### Task 2: Invoke Face API 

1.  Continuing with **MainWindow.xaml.cs**, scroll down to **GetBlobSasUri**. This method will create a Shared Access Signature URI that the Face API can use to securely access the image in blob storage. 

    ![The GetBlobSasUrl method is high-lighted in Visual Studio](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image95.png)

2.  Replace TODO 4 with the following:
    ```
    //TODO: 4. Create a Read blob and Write blob Shared Access Policy that is effective 5 minutes ago and for 2 hours into the future
    SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
    sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
    sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2);
    sasConstraints.Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write;
    ```

3.  Replace TODO 5 with the following:
    ```
    //TODO: 5. construct the full URI with SAS
    string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);
    return blob.Uri + sasBlobToken;
    ```

4.  With the SAS URI to the upload photo in hand, scroll to **GetPhotoDemographics** to implement the call to the Face API. 

    ![GetPhotoDemographics is high-lighted in Visual Studio.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image96.png)

5.  Replace TODO 6 with the following:
    ```
    //TODO 6. Invoke Face API with URI to photo
    IFaceServiceClient faceServiceClient = new FaceServiceClient(_faceApiKey, _faceEndpoint);
    ```

6.  Replace TODO 7 with the following:
    ```
    //TODO 7. Configure the desired attributes Age and Gender
    IEnumerable<FaceAttributeType> desiredAttributes = new FaceAttributeType[] { FaceAttributeType.Age, FaceAttributeType.Gender };
    ```

7.  Replace TODO 8 with the following:
    ```
    //TODO 8. Invoke the Face API Detect operation
    Face[] faces = await faceServiceClient.DetectAsync(sasUri, false, true, desiredAttributes);
    ```

8.  Replace TODO 9 with the following:
    ```
    //TODO 9. Extract the age and gender from the Face API response
    double computedAge = faces[0].FaceAttributes.Age;
    string computedGender = faces[0].FaceAttributes.Gender;
    ```

9.  Save the file.

### Task 3: Invoke pricing model

1.  Within **MainWindow.xaml.cs**, scroll to the end of **UpdateDynamicPrice** and replace TODO 10 with the following:
    ```
    //TODO 10. Invoke the actual ML Model
    PricingModelService pricingModel = new PricingModelService();
    string gender = d.gender == "Female" ? "F" : "M";
    suggestedPrice = await pricingModel.GetSuggestedPrice((int)d.age, gender, _itemName);
    ```

2.  Save the file.

### Task 4: Configure the Simulator

1.  In the Simulator project, open **App.config**.

2.  Within the **appSettings** section, set the following settings (there were copied into a text edit previously):

    a.  **faceAPIKey**: set this to the KEY 1 value for your Face API as acquired from the Azure Portal.

    b.  **faceEndpoint**: set this to the ENDPOINT value for your Face API as acquired from the Azure Portal (for example: https://eastus2.api.cognitive.microsoft.com/face/v1.0)

    c.  **storageConnectionString**: set this to the connection string of the Storage Account you created with the photos container.

3.  Save the **App.config**. The updated **App.config** file settings should look similar to the following:

    ![Screenshot of the App.config file setting. At this time, we are unable to capture all of the information in the window. Future versions of this course should address this.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image97.png "App.config file setting")

### Task 5: Test dynamic pricing in Simulator

1.  Before building the project in Visual Studio, we need to ensure all the NuGet packages are properly restored. In Visual Studio, go to **Tools-\>NuGet Package Manager-\>Package Manager Console** and enter the following command:

**Update-Package -Reinstall**

2.  That should force the packages to get downloaded again.

3.  Now, in Solution Explorer, right-click the Simulator project, and select **Build**.

4.  Ensure that your build generates no errors (View the Output and Errors windows, under the View menu in Visual Studio).

5.  Again, in solution explorer, right-click the Simulator project, and select Set as Startup Project.

    ![Screenshot of Solution Explorer, with Simulator selected, and in the sub-menu, Set as StartUp Project selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image98.png "Solution Explorer")

6.  From the Debug menu, select Start Without Debugging.
    
    ![Screenshot of the Debug menu, with Start Without Debugging selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image99.png "Debug menu")

7.  When the vending machine simulator appears, **select take picture** at the bottom.
    
    ![The Vending Machine Simulator displays the ad for coconut water for \$1.25.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image100.png "Vending Machine Simulator")

8.  In the dialog that appears, navigate to the images folder under **C:\\VendingMachines\\VendingMachines - Clean\\Simulator\\images\\photos**, and pick the photo of either the man or woman to upload, and select Open.
    
    ![Screenshot of Open window, expanded to the photos folder where two images display: female.jpeg, and male.jpeg.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image101.png "Photos folder")

9.  In a few moments, you should see the price change from \$1.25 to whatever value the predictive model suggested. 

    ![The Vending Machine Simulator now displays the coconut water price as \$0.91.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image102.png "Vending Machine Simulator")

10. Try using the other photo or your own photo to see what prices are suggested.

11. Click the X at the top right of the application to stop it.

## Exercise 4: Implement purchasing

Duration: 15 minutes

In this exercise, you will create an in-memory, columnar index table in SQL DB that will be used to support purchase transactions in a real-time analytics fashion, and then implement the purchasing process in the vending machine simulator. Finally, you will run the simulator and purchase items.

### Task 1: Create the transactions table

1.  Within Visual Studio Solution Explorer, expand the SQL Scripts folder and open the file Create Table.sql.
    
    ![Screenshot of the Visual Studio Solution Explorer expanded to Create Table.sql.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image103.png "Visual Studio Solution Explorer")

2.  Replace TODO 1 with the following:
    ```
    TODO: 1. Transaction ID should be a Primary Key, fields with a b-tree index
    TransactionId int IDENTITY NOT NULL PRIMARY KEY NONCLUSTERED,
    ```

3.  Replace TODO 2 with the following:
    ```
    TODO: 2. This table should have a columnar index
    INDEX Transactions_CCI CLUSTERED COLUMNSTORE
    ```

4.  Replace TODO 3 with the following:
    ```
    TODO: 3. This should be an in-memory table
    MEMORY_OPTIMIZED = ON
    ```

5.  Replace TODO 4 with the following:
    ```
    TODO: 4. In-memory tables should auto-elevate their transaction level to Snapshot
    ALTER DATABASE CURRENT SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON;
    ```

6.  Save the script.

7.  Execute the script by pressing the play icon.

    ![Play button arrow](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image104.png "Play button arrow")

8.  Expand **Azure**, and if prompted, sign in with your Azure credentials.

9.  From the Azure node, and select the database you created for the vending database. 

    ![Screenshot of the Connect window. Azure is expanded, and vending (vendingmachineslab) is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image105.png "Connect window")

10. In the in fields at the bottom, enter your user name and password for the SQL Server, and select Connect. The script should run successfully.

    ![Screenshot of the Query executed successfully message.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image106.png "Query executed successfully message")

### Task 2: Configure the Simulator

1.  In the Simulator project, open **App.config**.

2.  Within the **connectionString** section, set the following:

    -   TransactionsModel: set the value of the connectionString attribute to the ADO.NET connection string to your SQL DB instance. This value was copied to a text editor previously, or you can copy it from the Azure Portal. **Do not forget to replace the values for {your\_username} and {your\_password} with your actual credentials.**

        i.  User name: demouser

        ii. Password: Password.1!!

1.  Save the **App.config**. 

    ![The App.config file is shown in Visual Studio with the updated connection string to the database.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image107.png)

### Task 3: Test purchasing

1.  In solution explorer, right-click the Simulator project, and select Build. Note: You may need to ensure the previous instance you started has been closed before rebuilding.

2.  Ensure that your build generates no errors.

3.  From the Debug menu, select **Start Without Debugging**.

4.  In the Simulator, select buy.
    
    ![The Vending Machine Simulator displays the ad for coconut water for \$1.25, with a buy button.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image100.png "Vending machine simulation")

5.  You should see a confirmation dialog similar to the following:\
    
    ![Screenshot of the Purchase Complete dialog box, with the message \"Enjoy!\"](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image108.png "Purchase Complete dialog box")

## Exercise 5: Implement device command and control

Duration: 30 minutes

In this exercise, you will implement the ability to push new promotions to the vending machine simulator using the command and control features of IoT Hub. You will update the simulator to listen for these messages. You will also update the console application DeviceControlConsole to send selected promotions.

### Task 1: Listen for control messages

1.  Within Visual Studio Solution Explorer, expand the Simulator project, and open the file **MainWindow.xaml.cs**.

2.  Scroll down to the **ListenForControlMessages** method. 

    ![In the Visual Studio Solution Explorer window, \"ListenForControlMessages\" is circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image109.png "Visual Studio Solution Explorer")

3.  Uncomment the body of the while(true) loop. You can uncomment a block of code by selecting the code, then selecting the Uncomment button on the toolbar. 

    ![Screenshot of the Solution Explorer toolbar, with the Uncomment button circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image110.png "Solution Explorer toolbar")

4.  Replace TODO 1 with the following:
    ```
    //TODO: 1. Receive messages intended for the device via the instance of _deviceClient.
    Microsoft.Azure.Devices.Client.Message receivedMessage = await _deviceClient.ReceiveAsync();
    ```

5.  Replace TODO 2 with the following:
    ```
    //TODO: 2. A null message may be received if the wait period expired, so ignore and call the receive operation again
    if (receivedMessage == null) continue;
    ```

6.  Replace TODO 3 with the following:
    ```
    //TODO: 3. Deserialize the received binary encoded JSON message into an instance of PromoPackage.
    string receivedJSON = Encoding.ASCII.GetString(receivedMessage.GetBytes());
    System.Diagnostics.Trace.TraceInformation("Received message: {0}", receivedJSON);
    PromoPackage promo = Newtonsoft.Json.JsonConvert.DeserializeObject<PromoPackage>(receivedJSON);
    ```

7.  Replace TODO 4 with the following:
    ```
    //TODO: 4. Acknowledge receipt of the message with IoT Hub
    await _deviceClient.CompleteAsync(receivedMessage);
    ```

8.  Save the file.

### Task 2: Send control messages

1.  Within Visual Studio Solution Explorer, expand the **DeviceControlConsole** project, and open the file **Program.cs**. 

    ![Screenshot of Visual Studio Solution Explorer, with DeviceControlConsole expanded, and Program.cs circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image111.png "Visual Studio Solution Explorer")

2.  Scroll down to the PushPromo method. 

    ![PushPromo method](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image112.png "Visual Studio Solution Explorer")

3.  Replace TODO 1 with the following:
    ```
    //TODO: 1. Create a Service Client instance provided the _IoTHubConnectionString
    _serviceClient = ServiceClient.CreateFromConnectionString(_IoTHubConnectionString);
    ```

4.  Replace TODO 2 with the following:
    ```
    //TODO: 2. Send the command
    await _serviceClient.SendAsync(deviceId, commandMessage);
    ```

5.  Save Program.cs.

### Task 3: Configure the DeviceControlConsole and the Simulator

1.  In **DeviceControlConsole**, open **App.config**.

    ![Screenshot of Visual Studio Solution Explorer, with DeviceControlConsole expanded, and App.config circled.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image113.png "Visual Studio Solution Explorer")

2.  Set the **IoTHubConnectionString appSetting** to have a value of the connection string for the service policy to your IoT Hub. (Recall you can get this from the Azure Portal IoT Hub blade, Shared access policies, and then select the policy.)

    ![Screenshot of the iothubowner blade. A callout points to the copy button to the right of the connection string - primary key field.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image40.png "iothubowner blade")

3.  Set the **storageConnectionString appSetting** to have the same connection string for your storage account that the **App.config** file in the Simulator project has.

4.  Save the file.

5.  Now, open the **App.config** file In the Simulator project.

6.  Set the **IoTHubSenderConnectionString appSetting** to have a value of the connection string for the device policy to your IoT Hub.

7.  Set the **IoTHubManagerConnectionString appSetting** to have a value of the connection string for the **iothubowner** policy to your IoT Hub.

8.  Save the file.

9.  Build the Simulator and DeviceControlConsole projects.

10. In Solution Explorer, right-click Solution 'Vending Machines,' and select Set StartUp Projects.

    ![Screenshot of the Solution Explorer sub-menu for Solution \'VendingMachines\' (3 projects). Set StartUp Projects is selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image114.png "Solution Explorer")

11. In the dialog, select the Multiple startup projects option, and ensure that Action is set to Start for both DeviceControlConsole and Simulator.
    
    ![Screenshot of the Solution \'VendingMachines\' Property Pages Dialog Box.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image115.png "Solution 'VendingMachines' Property Pages Dialog Box")

12. Select OK.

13. From the Debug menu, choose Start without Debugging.

14. Wait for both the Vending Machine Simulator and the DeviceControlConsole to appear.
   
    ![Screenshot of the Vending Machine Simulator and DeviceControlConsole. The Vending Machine Simulator displays the coconut water ad.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image116.png "Vending Machine Simulator and DeviceControlConsole")

15. In the DeviceControlConsole, press 1 to push the promotion for Soda.

    ![Screenshot of the Vending Machine Simulator and DeviceControlConsole. This time, the Vending Machine Simulator displays the soda ad.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image117.png "Vending Machine Simulator and DeviceControlConsole")

16. Observe that the entire promotion surface of the vending machine changes (product name, price, and image).

**Note:** If the photo does not change, and after a few minutes you receive a DotNetty.Transport\... error, you will need to delete and recreate your IoT Hub in the Azure portal. The error is caused by a communication error between the application and your IoT Hub. Be sure to update your App.config file with the new IoT hub connection strings.

17. Experiment sending the other promotion or toggling between promotions.

18. Experiment with making purchases and sending photos to verify the other functions still work with the new promoted products.

## Exercise 6: Analytics with Power BI Desktop

Duration: 15 minutes

In this exercise, you will use Power BI Desktop to query purchase data from the in-memory table of SQL DB and visualize the result.

### Task 1: Build the query and create the visualization

1.  From your Start menu on your Lab VM, open Power BI Desktop.
    
    ![The Powerl BI Desktop link is shown.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image118.png)

2.  In the opening dialog, select **Get Data**.

    ![Screenshot of the Power BI Desktop opening dialog box, with the Get Data link.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image119.png "Power BI Desktop opening dialog box")

3.  In the Get Data dialog, select **Azure** in the categories list and then **Azure SQL Database**.
    
    ![Screenshot of the Get Data dialog box.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image120.png "Get Data dialog box")

4.  Select Connect.

5.  In the dialog, enter the name of your SQL Server (e.g., myserver.database.windows.net), the name of your vending database, and select the **DirectQuery** option. Select OK.
    
    ![Screenshot of the SQL Server Database dialog box.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image121.png "SQL Server Database dialog box")

6.  On the next screen, select the **Database** tab on the left, and provide your SQL username (demouser) and password (Password.1!!), then select **Connect**. 
    
    ![Screenshot of the SQL Server Database dialog box.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image122.png "SQL Server database dialog box")

7.  In the Navigator dialog, check the box next to **Transactions**.
    
    ![Screenshot of the Navigator dialog box. In the left pane, under Display options, the checkbox is selected for Transactions.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image123.png "Navigator dialog box")

8.  Select **Load**.

9.  In the Ribbon, select **Edit Queries**.

    ![The Edit Queries button is shown.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image124.png)

10. In the Query Editor, select the Transaction Date column header to select the column.
    
    ![The TransactionDate column is shown.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image125.png)

11. In the Ribbon, select the **Add Column** tab and select Time, Hour, Hour. 

    ![Screenshot of the Query Editor. On the ribbon, the Add Column tab is selected. On the Add Column ribbon, Time is selected. From it\'s sub-menu, Hour is selected, and from Hour\'s sub-menu, Hour is selected again.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image126.png "Query Editor")

12. Select the **TransactionDate** column again.

13. In the Ribbon, select **Time, Minute.**\
    
    ![On the Add Column ribbon, Time is selected, and from it\'s sub-menu, Minute is selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image127.png "Time sub-menu's")

14. Select the **TransactionDate** one more time.

15. In the Ribbon, select **Time, Second**.

16. In the Ribbon, on the Home tab, select **Close & Apply**.
    
    ![Close and Apply is shown.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image128.png)

17. In the message that appears, select **Apply Changes**.

    ![The Apply Changes message displays, informing you that there are pending changes in your queries that haven\'t been applied.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image129.png "Apply Changes message")

18. In the Visualizations, select **Stacked column chart**.
    
    ![Screenshot of the Visualizations menu, with the stacked column chart icon selected.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image130.png "Visualizations menu")

19. From the Fields list, drag the **minute** field over to the **axis** property.

    ![Minute has been added under Axis.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image131.png)

20. From the Fields list, drag the **PurchasePrice** over to the **value** property.
    
    ![The PurchasePrice has been added under Value.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image132.png)

21. Your completed visualization summarizing the most profitable minutes in each hour should appear as follows:

    ![Screenshot of a stacked column chart detailing the purchase price by minute. At this time, we are unable to capture all of the information in the chart. Future versions of this course should address this.](images/Hands-onlabstep-by-step-Intelligentvendingmachinesimages/media/image133.png "stacked column chart")

## After the hands-on lab 

Duration: 10 mins

In this exercise, attendees will deprovision any Azure resources that were created in support of the lab. You should follow all steps provided after attending the Hands-on lab.

### Task 1: Delete the resource group

1.  Using the Azure portal, navigate to the Resource group you used throughout this hands-on lab by selecting Resource groups in the left menu.

2.  Search for the name of your research group, and select it from the list.

3.  Select Delete in the command bar, and confirm the deletion by re-typing the Resource group name, and selecting Delete.

