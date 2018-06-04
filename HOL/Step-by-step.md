![Microsoft Cloud Workshop](../media/ms-cloud-workshop.png "Microsoft Cloud Workshop")

[Legal notice](../legal.md)

Updated May 2018

# Intelligent vending machines hands-on lab step-by-step

In this hands-on lab, you will implement an IoT solution for intelligent vending machines, leveraging facial feature recognition and Azure Machine Learning, to gain a better understanding of building cloud-based machine learning apps and real-time analytics with SQL Database in-memory and columnar indexing.

If you have not yet completed the steps to set up your environment in [Before the hands-on lab](./Setup.md), you will need to do that before proceeding.

## Contents

* [Exercise 1: Create Dynamic Pricing Model](#exercise-1-create-dynamic-pricing-model)
  * [Task 1: Create a model locally](#task-1-create-a-model-locally)
  * [Task 2: Try a prediction locally](#task-2-try-a-prediction-locally)
  * [Task 3: Create the model in R Server on HDInsight](#task-3-create-the-model-in-r-server-on-hdinsight)
  * [Task 4: Create predictive service in R Server Operationalization](#task-4-create-predictive-service-in-r-server-operationalization)

* [Exercise 2: Implement dynamic pricing](#exercise-2-implement-dynamic-pricing)
  * [Task 1: Implement photo uploads to Azure Storage](#task-1-implement-photo-uploads-to-azure-storage)
  * [Task 2: Provision Cognitive Services Face API](#task-2-provision-cognitive-services-face-api)
  * [Task 3: Invoke Face API](#task-3-invoke-face-api)
  * [Task 4: Invoke pricing model](#task-4-invoke-pricing-model)
  * [Task 5: Configure the Simulator](#task-5-configure-the-simulator)
  * [Task 6: Test dynamic pricing in Simulator](#task-6-test-dynamic-pricing-in-simulator)

* [Exercise 3: Implement purchasing](#exercise-3-implement-purchasing)
  * [Task 1: Create the transactions table](#task-1-create-the-transactions-table)
  * [Task 2: Configure the Simulator](#task-2-configure-the-simulator)
  * [Task 3: Test purchasing](#task-3-test-purchasing)

* [Exercise 4: Implement device command and control](#exercise-4-implement-device-command-and-control)
  * [Task 1: Provision IoT Hub](#task-1-provision-iot-hub)
  * [Task 2: Listen for control messages](#task-2-listen-for-control-messages)
  * [Task 3: Send control messages](#task-3-send-control-messages)
  * [Task 4: Configure the DeviceControlConsole and Simulator](#task-4-configure-the-devicecontrolconsole-and-simulator)

* [Exercise 5: Analytics with Power BI Desktop](#exercise-5-analytics-with-power-bi-desktop)
  * [Task 1: Build the query and create the visualization](#task-1-build-the-query-and-create-the-visualization)

## Exercise 1: Create Dynamic Pricing Model

Duration: 45 minutes

In this exercise, you will create a machine learning model that predicts the purchase price for an item sold by the vending machine, provided the demographics of the customer and the item. You will then operationalize this model by exposing it as a web service hosted in Azure Machine Learning, and test it out.

### Task 1: Create a model locally

1. On your Lab VM, locate the starter project solution file, `VendingMachines.sln`, in the C:\\VendingMachines\\HOL\starter-project folder and double-click it to open it with Visual Studio 2017.

2. If prompted, select Visual Studio 2017 from the Microsoft Visual Studio Version Selector.

    ![Visual Studio 2017 in selected in the Microsoft Visual Studio Version Selector.](./media/visual-studio-version-selector.png "Microsoft Visual Studio Version Selector")

3. Sign in to Visual Studio or create an account, if prompted.

4. If the Security Warning for Simulator window appears, uncheck **Ask me for every project in this solution**, and select **OK**.

    ![Screenshot of the Security warning for Simulator window. The checkbox is cleared for "Ask me for every project in this solution."](./media/visual-studio-security-warning.png "Security warning for Simulator")

    > Note: If you attempt to build the solution at this point, you will see many build errors. This is intentional. You will correct these in the exercises that follow.

5. Within Visual Studio Solution Explorer, expand the **PricingModel** project and open the file `TrainModel.R` by double-clicking on the file in the Solution Explorer.

    ![Screenshot of the Visual Studio Solution Explorer, expanded to: Solution VendingMachines\\PricingModel\\TrainModel.R.](./media/visual-studio-solution-explorer-trainmodel.png "Visual Studio Solution Explorer")

6. Read the script. The top portion, entitled Create Sample Data, has been provided for you and you will generate the sample data you will use to train your model.

7. Highlight all the text between the "Create Sample Data" and "END Create Sample Data" comments.

8. Right-click the selected text and select **Execute In Interactive**.

    ![Screenshot of the Train Model tab, with Execute In Interactive highlighted on the sub-menu.](./media/visual-studio-execute-in-interactive.png "Train Model tab")

9. You should see it execute in the R Interactive Window, ending with a summary of the created data.

    ![Screenshot of the R Interactive - Microsoft R Client window. ](./media/visual-studio-r-interactive-sample-data.png "R Interactive - Microsoft R Client window")

10. From the R Tools menu, select Windows, then select Variable Explorer.

    ![Screenshot of the R tools menu, with Windows / Variable Explorer selected.](./media/visual-studio-r-tools-menu-variable-explorer.png "R tools menu")

11. Expand the variable **sampleData**, and explore the structure of the created data.

    ![Screenshot of the Variable Explorer, with sampleData selected.](./media/visual-studio-r-tools-variable-explorer.png "Variable Explorer")

12. Now save this sampleData to a file by replacing `TODO 1` in the `TrainModel.R` script with the following code:

    ```r
    # TODO: 1. Export the sample data to a file
    save(sampleData, file = "sampleData.RData")
    ```

13. Highlight the **save line**, and select **Execute In Interactive**.

14. Open File Explorer and navigate to the location of the **PricingModel** (C:\\VendingMachines\\HOL\\starter-project\\PricingModel) project on disk. You should see the file `sampleData.RData` on disk.

    ![Screenshot of File Explorer, with sampleData.RData selected, and the pop-up listing the Type (R Workspace), Size (2.85 KB), and Data/time modified.](./media/file-explorer-sampledata-rdata.png "File Explorer, sampleData.RData")

15. Back in the `TrainModel.R` file in Visual Studio, replace `TODO 2` with the following code that builds the model using a Linear Regression.

    ```r
    # TODO: 2. Build a linear regression model to predict purchase price given age, gender and # productSelect
    pricingModel <- rxLinMod(purchasePrice ~ age + gender + productSelected, data = sampleData)
    ```

16. Save that trained model to disk by replacing `TODO 3` with:

    ```r
    # TODO: 3. Export the trained model to a file named pricingModel.rda
    save(pricingModel, file = "pricingModel.RData")
    ```

17. Finally, save the first row of the sample data to a file so you can re-use the structure later when operationalizing the model. Replace `TODO 4` with:

    ```r
    # TODO: 4. Save one example of the sample data to serve as an input template, to a file
    # called inputExample.rda
    inputExample <- sampleData[1,]
    save(inputExample, file = "inputExample.RData")
    ```

18. Save your changes to `TrainModel.R`.

19. Highlight TODO items 2 through 4 and execute them in interactive.

20. In the same folder as your script, you should now have the files `sampleData.RData`, `pricingModel.RData`, and `inputExample.RData`.

    ![Screenshot of File Explorer, with the previously mentioned files highlighted.](./media/visual-studio-r-output-data-files.png "File Explorer")

### Task 2: Try a prediction locally

1. Within Visual Studio, open `PredictUsingModel.r` under the Pricing Model project in Solution Explorer.

2. Replace `TODO 1` with the following:

    ```r
    # TODO: 1. Prepare the input to use for prediction
    inputExample[1,]$age <- 30
    inputExample[1,]$gender <- "F"
    inputExample[1,]$productSelected <- "coconut water"
    ```

3. Replace `TODO 2` with the following:

    ```r
    # TODO: 2. Execute the prediction
    prediction <- rxPredict(pricingModel, data = inputExample)
    ```

4. Highlight all the script in the file and execute it in interactive.

5. Using Variable Explorer, expand the prediction variable and observe the price the model suggested to use for purchasing the coconut water for input of a 30-year-old female.

    ![Screenshot of Variable Explorer, prediction table. The purchasePrice_Prec is 0.949.](./media/visual-studio-r-prediction.png "Variable Explorer")

### Task 3: Create the model in R Server on HDInsight

1. On your Lab VM, open a Git Bash shell from the start menu.

2. At the command prompt, you will enter the following command to create the SSH connection:

    ```bash
    ssh remoteuser@<clustername>-ssh.azurehdinsight.net
    ```

    > Replace \<clustername\> with the name of your HDInsight cluster, which you can obtain from the Overview blade of your HDInsight cluster in the Azure portal.

    ![Screenshot of the HDInsight cluster blade for vendingmachineslab. Overview is selected in the left pane, and in the right pane, on the menu bar, Secure Shell (SSH) is highlighted.](./media/hdinsight-menu-ssh.png "HDInsight cluster blade")

    ![Screenshot of Connect to cluster. Ssh.azurehdinsight.net is highlighted in the Hostname field, with the endpoint highlighted below.](./media/hdinsight-connect-to-cluster-with-ssh.png "Connect to cluster section")

3. If prompted to continue connecting, enter yes.

4. Enter the password for **remoteuser** (Password: Password.1!!).

    ![Screenshot of the Git Bash window, with three command lines highlighted. The first is the remoteuser@clustername information. The second line is a prompt asking whether you want to continue connecting. The third is the remoteuser cluster name request for password.](./media/git-bash-ssh-to-hdinsight-cluster.png "Git Bash window")

5. At the command prompt, type **R** to load the R shell (be sure to use a capital letter "R").

6. Run the following command to create a spark context for R:

    ```r
    sparkCluster <- RxSpark()
    rxSetComputeContext(sparkCluster)
    ```

7. In Visual Studio, open `TrainModel.r`, and copy the entire script.

8. Paste the script in the R shell, and press ENTER. (You may need to press ENTER a few times until you get to the last line of the script.)

9. When the script has finished executing, type the following:

    ```r
    dir()
    ```

10. You should see it list the three files created by the script, as follows:

    ![Screenshot of the results of running the TrainModel.R script in R shell, listing the following three files: inputExample.RData, pricingModel.RData, and sampleData.RData.](./media/git-bash-trainmodel-r-result.png "Results of TrainModel.R script in R shell")

11. Now, copy those files from local storage to Blob storage by using the Hadoop File System. First, create a folder in which to store your output.

    ```r
    modelExportDir <- "/models"
    rxHadoopMakeDir(modelExportDir)
    ```

12. List the contents of the root ("/") directory, and confirm your "**/models**" folder has been created. Notice that the list you are looking at is folders directly underneath the container in Azure Storage that was created with your cluster.

    ```r
    rxHadoopListFiles("/")
    ```

    ![Screenshot of the Git Bash R shell window, with the previously mentioned information displaying, and the /models folder information highlighted.](./media/git-bash-r-shell-file-list.png "Git Bash R Shell")

13. Copy the `pricingModel.RData` from the local directory to the **/models** folder in HDFS by running the following command:

    ```r
    rxHadoopCopyFromLocal("pricingModel.RData", modelExportDir)
    ```

14. Repeat the previous step for `inputExample.RData` and `sampleData.RData`.

15. Run the following command to verify the three files now exist in HDFS (and Blob storage), under /models.

    ```r
    rxHadoopListFiles("/models")
    ```

16. The output should look similar to the following:

    ![Screenshot of the Git Bash R Shell window with output verifying that the three files exist in HDFS.](./media/git-bash-r-shell-models-file-list.png "Git Bash R Shell")

17. Using Visual Studio, Cloud Explorer, navigate to the storage account for your HDInsight cluster, expand:

    ![Screenshot of Visual Studio, Cloud Explorer expanded as: labstorageSUFFIX\\Blob Containers\\vendingmachinescluster.](./media/visual-studio-cloud-explorer-lab-storage-container.png "Cloud Explorer storage container")

18. Right-click the storage container, and select **Open.**

19. In the editor that appears, double-click the models folder, and verify you see your files.

    ![Cloud Explorer file explorer displaying files listed under models/; three files display: inputExampleRData, pricingModelRData, and sampleDataRData.](./media/visual-studio-cloud-explorer-models-folder.png "Cloud Explorer Files")

20. Right-click `inputExample.RData` and select Save As... and choose the directory for your `PricingModel` project, overwriting files if prompted.

21. Repeat the previous step for `pricingModel.RData` and `sampleData.RData`.

22. You have now used R Server on HDInsight to train a model that you can then upload to R Server Operationalization to expose it as a web service.

### Task 4: Create predictive service in R Server Operationalization

After training a model, you want to operationalize the model so that it becomes available for integration by developers. One way to operationalize a trained model is to take the model you trained in HDInsight, and then to expose that as a predictive web service. In this task, you take a version of the scripts you have been running locally and in HDInsight and migrate them to run in the VM that is running R.

1. In the [Azure portal](https://portal.azure.com), navigate to the Microsoft Machine Learning Server Virtual Machine, named LabMLServer, you provisioned with the ARM template.

2. On top of the Overview blade, select Connect, then copy the SSH command.

    ![The Connect dialog for the Microsoft Machine Learning Server is displayed, and the SSH command has been copied.](./media/machine-learning-server-ssh-command-copied.png "SSH Command copied")

3. Using a new Git Bash window on your Lab VM, SSH into your Microsoft Machine Learning Server VM by pasting the SSH command you copied above at the command prompt. For example:
    **ssh radmin@[your-server-ip]**

4. When prompted if you want to continue connecting, enter yes.

5. Enter your password, **Password.1!!**

6. At the prompt, after successfully logging in, you will need to complete a few tasks to configure and operationalize the environment.

7. Run the following command to act as root:

    ```bash
    sudo -i
    ```

8. Now that you are acting as root run the following command to:

    ```bash
    az ml admin node setup --onebox --admin-password Password.1!! --confirm-password Password.1!!
    ```
    > If the above command gives you issues, you can also try running the following, and then enter the password when prompted:

    ```bash
    az ml admin node setup --onebox
    ```

    ![Command to setup ML server to host a webservice has been completed. the port 12800 is highlighted.](./media/git-bash-setup-ml-web-service.png "Web Service Listening")

9. In Visual Studio, open the `App.config` for the Simulator project. This can be done by expanding the Simulator project in the Solution Explorer, and double-clicking on `App.config` under the **Simulator** project.

    ![The Visual Studio Solution Explorer has Simulator highlighted and expanded, and under it, App.config highlighted.](./media/visual-studio-solution-explorer-simulator-app-config.png "Visual Studio Solution Explorer")

10. Locate the `appSetting` `rServiceBaseAddress` and enter **<http://[your-server-public-ip]:12800>** for the value. (For example: <http://52.168.132.221:12800>). Your server IP address is the same IP address you used for the SSH connection in Step 3 of this task, above.

11. Locate **rServicePassword** in the appSettings section and update its value with the password you defined when you configured your R server for Operationalization (step 11 above), **Password.1!!**

    ![The App.config file for the project is open with the AppSettings for three keys shown. These include the URL to the server, user name and password.](./media/visual-studio-app-config-appsettings.png "App.config appSettings section")

12. Save `App.config`.

13. Open `PredictPricingService.r` within the **PricingModel** project in the Visual Studio Solution Explorer.

    ![The Visual Studio Solution Explorer has PricingModel expanded, and under it, PredictPricingService.r highlighted.](./media/visual-studio-solution-explorer-pricing-model-predict-pricing-service.png "Visual Studio Solution Explorer")

14. Find `TODO 1`, and replace with the following code block:

    ```r
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

15. Find `TODO 2`, and replace with the following code block to remotely connect to the R Server Operationalization service:

    ```r
    # TODO: 2. Configure remote login
    remoteLogin(
    deployr_endpoint = "http://<your-server-ip>:12800",
    username = "admin",
    password = "<your-admin-password>"
    )
    pause()
    ```

    > **Note:** Make sure to replace **\<your-server-ip\>** with the IP address with your VM's IP address, and enter the password you specified when you configured your R server for Operationalization (Password.1!!) in place of **\<your-admin-password\>**. The `TODO 2` section should looking something like the following screen shot.

    ![Screenshot of the TODO 2 section, with the IP address and password highlighted.](./media/visual-studio-predict-pricing-service-todo-2.png "TODO 2 section")

16. Highlight all the code in `PredictPricingService.r`, right-click and then **execute in interactive**. The last output status in the R Interactive window should be "Published service".

17. Find `TODO 3` and replace with the following code block to consume the API as a test:

    ```r
    # TODO: 3. Consume the API as a test
    services <- listServices("apiPredictPurchasePrice")
    serviceName <- services[[1]]
    api <- getService(serviceName$name, serviceName$version)
    result <- api$apiPredictPurchasePrice(30, "F", "coconut water")
    print("Result: ")
    print(result$output("answer"))
    result
    ```

18. Find `TODO 4` and replace with the following code block to generate and save the Swagger JSON file for the API:

    ```r
    # TODO: 4. Generate the Swagger JSON file for the API
    swagger <- api$swagger()
    cat(swagger, file = "swagger.json", append = FALSE)
    ```

19. Highlight just the `TODO 3` and `TODO 4` code blocks you added and execute in interactive.

20. When you scroll up through the R Interactive window results, you should see an output with your prediction like the following:

    ```r
    $success
    [1] TRUE

    $errorMessage
    [1] ""

    $outputParameters
    $outputParameters$purchasePrice
    [1] 0.9348741
    ```

21. Also open the `swagger.json` file in your PricingModel project directory to view its contents. This file can be used within the `swagger.io` online editor to generate client code to connect to your service. We have already done this for you within the included IO.Swagger project.

## Exercise 2: Implement dynamic pricing

Duration: 45 minutes

In this exercise, you will implement the code that performs dynamic pricing, capitalizing on the Face API to acquire demographics, and your deployed pricing model to suggest the price based on those demographics. You will then run the vending machine simulator and see the dynamic pricing in action.

### Task 1: Implement photo uploads to Azure Storage

1. In Visual Studio Solution Explorer, expand the Simulator project and then `MainWindow.xaml` and then open `MainWindow.xaml.cs`.

    ![Visual Studio Solution Explorer is expanded as: Simulator\\MainWindow.xaml\\MainWindow.xaml.cs.](./media/visual-studio-solution-explorer-simulator-mainwindowxaml-cs.png "Visual Studio Solution Explorer")

2. Scroll down to the method `UpdateDynamicPricing`.

    ![The UpdateDynamicPrice function is high-lighted in Visual Studio.](./media/visual-studio-solution-explorer-simulator-mainwindowxaml-updatedynamicprice.png "UpdateDynamicPrice function")

3. Replace `TODO 1` with the following:

    ```csharp
    // TODO 1. Retrieve storage account from connection string.
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
    CloudBlobContainer container = blobClient.GetContainerReference("photos");
    ```

4. Replace `TODO 2` with the following:
    ```csharp
    // TODO 2. Retrieve reference to a blob named with the value of fileName.
    string blobName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(filename);
    CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
    ```

5. Replace `TODO 3` with the following:
    ```csharp
    // TODO 3. Create or overwrite the blob with contents from a local file.
    using (var fileStream = System.IO.File.OpenRead(filename))
    {
        blockBlob.UploadFromStream(fileStream);
    }
    ```

6. Save `MainWindow.xaml.cs`.

### Task 2: Provision Cognitive Services Face API

To provision access to the Face API (which provides demographic information about photos of human subjects), you will need to provision a Cognitive Services account.

1. In the [Azure portal](https://portal.azure.com), select **+Create a resource**, enter "face" into the Search the Marketplace box, select **Face** from the results, and select **Create**.

    ![+Create a resource is highlighted in the navigation pane of the Azure portal, "face" is entered into the Search the Marketplace box, and Face is highlighted in the results.](./media/create-resource-cognitive-services-face-api.png "Create Face API")

2. On the Create Face API blade:

    * **Name**: Enter a name, such as vendingfaceapi

    * **Subscription**: Select the subscription you are using for this hands-on lab

    * **Location**: Select the location you are using for this hands-on lab

    * **Pricing tier**: Select F0

    * **Resource group**: Choose Use existing and select the hands-on-lab-SUFFIX resource group

        ![The Face API Create blade is displayed, and the values specified above are entered into the appropriate fields.](./media/cognitive-services-face-api-create.png "Create Face API")

    * Select **Create**

3. When the Face API finishes provisioning, browse to the Cognitive Services Face API by clicking on Go to resource in the Deployment succeeded notification.

    ![Screenshot of the Deployment succeeded message, with the Go to resource button highlighted.](./media/cognitive-services-face-api-deployment-succeeded.png "Deployment succeeded message")

4. On top of the Cognitive Services overview blade, click the Copy button to the right of the Endpoint. Paste this value into a text editor, such as Notepad, for later use.

    ![Screenshot of the Cognitive Services overview blade, Essentials section. The Endpoint url is highlighted, and a callout points to the "Click to Copy" button.](./media/azure-portal-cognitive-services-overview-endpoint.png "Cognitive Services overview blade, Essentials section")

5. In the Cognitive Services blade, select on Keys under the Resource Management heading.

    ![Screenshot of the Keys button.](./media/azure-portal-cognitive-services-keys.png "Keys button")

6. Click the Copy button next to the value for Key 1. Paste this value into a text editor, such as Notepad, for later use.

    ![The Azure portal is shown with Key copied for the API.](./media/azure-portal-cognitive-services-keys-key1.png "Copy Face API Keys")

### Task 3: Invoke Face API

1. Continuing with `MainWindow.xaml.cs`, scroll down to `GetBlobSasUri`. This method will create a Shared Access Signature URI that the Face API can use to securely access the image in blob storage.

    ![The GetBlobSasUri method is high-lighted in Visual Studio](./media/visual-studio-mainwindowxaml-getblobsasuri.png "GetBlobSasUri method")

2. Replace `TODO 4` with the following:

    ```csharp
    //TODO: 4. Create a Read blob and Write blob Shared Access Policy that is effective 5 minutes ago and for 2 hours into the future
    SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
    sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
    sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2);
    sasConstraints.Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write;
    ```

3. Replace `TODO 5` with the following:

    ```charp
    //TODO: 5. construct the full URI with SAS
    string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);
    return blob.Uri + sasBlobToken;
    ```

4. With the SAS URI to the upload photo in hand, scroll to `GetPhotoDemographics` to implement the call to the Face API.

    ![GetPhotoDemographics is high-lighted in Visual Studio.](./media/visual-studio-mainwindowxaml-getphotodemographics.png "GetPhotoDemographics method")

5. Replace `TODO 6` with the following:

    ```csharp
    //TODO 6. Invoke Face API with URI to photo
    IFaceServiceClient faceServiceClient = new FaceServiceClient(_faceApiKey, _faceEndpoint);
    ```

6. Replace `TODO 7` with the following:

    ```csharp
    //TODO 7. Configure the desired attributes Age and Gender
    IEnumerable<FaceAttributeType> desiredAttributes = new FaceAttributeType[] { FaceAttributeType.Age, FaceAttributeType.Gender };
    ```

7. Replace `TODO 8` with the following:

    ```csharp
    //TODO 8. Invoke the Face API Detect operation
    Face[] faces = await faceServiceClient.DetectAsync(sasUri, false, true, desiredAttributes);
    ```

8. Replace `TODO 9` with the following:

    ```csharp
    //TODO 9. Extract the age and gender from the Face API response
    double computedAge = faces[0].FaceAttributes.Age;
    string computedGender = faces[0].FaceAttributes.Gender;
    ```

9. Save the file.

### Task 4: Invoke pricing model

1. Within `MainWindow.xaml.cs`, scroll to the end of `UpdateDynamicPrice` and replace `TODO 10` with the following:

    ```csharp
    //TODO 10. Invoke the actual ML Model
    PricingModelService pricingModel = new PricingModelService();
    string gender = d.gender == "Female" ? "F" : "M";
    suggestedPrice = await pricingModel.GetSuggestedPrice((int)d.age, gender, _itemName);
    ```

2. Save the file.

### Task 5: Configure the Simulator

1. In the Simulator project, open `App.config`.

2. Within the `appSettings` section, set the following settings (there were copied into a text edit previously):

    * **faceAPIKey**: set this to the KEY 1 value for your Face API as acquired from the Azure Portal.

    * **faceEndpoint**: set this to the ENDPOINT value for your Face API as acquired from the Azure Portal (for example: <https://eastus2.api.cognitive.microsoft.com/face/v1.0>)

    * **storageConnectionString**: set this to the connection string of the Storage Account you created with the photos container.

        * On your Storage account blade. Select **Access Keys** from the left-hand menu.

            ![Screenshot of the Settings section on the Storage blade. Access keys is highlighted.](./media/azure-storage-account-access-keys.png "Storage blade, Settings section")

        * Use the copy button to the right of the Connection String for key1 to copy your storage connection string. Save the copied value to a text editor, such as Notepad, as this will be used later on.

           ![The keys for the storage account are shown in the Azure portal. The key1 Connection string is highlighted.](./media/azure-storage-account-access-keys-key1-connectionstring.png "Storage account access keys")

3. Save the `App.config`. The updated `App.config` file settings should look similar to the following:

    ![Screenshot of the App.config file setting. At this time, we are unable to capture all of the information in the window. Future versions of this course should address this.](./media/visual-studio-simulator-project-app-config.png "App.config file setting")

### Task 6: Test dynamic pricing in Simulator

1. Before building the project in Visual Studio, we need to ensure all the NuGet packages are properly restored. In Visual Studio, go to **Tools-\>NuGet Package Manager-\>Package Manager Console** and enter the following command:

    ```powershell
    Update-Package -Reinstall
    ```

2. That will force the packages to get downloaded again.

3. Now, in Solution Explorer, right-click the Simulator project, and select **Build**.

4. Ensure that your build generates no errors (View the Output and Errors windows, under the View menu in Visual Studio).

5. Again, in solution explorer, right-click the Simulator project, and select Set as Startup Project.

    ![Screenshot of Solution Explorer, with Simulator selected, and in the sub-menu, Set as StartUp Project selected.](./media/visual-studio-simulator-project-set-as-startup.png "Solution Explorer")

6. From the Debug menu, select Start Without Debugging.

    ![Screenshot of the Debug menu, with Start Without Debugging selected.](./media/visual-studio-debug-menu-start-without-debugging.png "Debug menu")

7. When the vending machine simulator appears, **select take picture** at the bottom.

    ![The Vending Machine Simulator displays the ad for coconut water for $1.25.](./media/simulator-app-coconut-water.png "Vending Machine Simulator")

8. In the dialog that appears, navigate to the images folder under **C:\\VendingMachines\\HOL\\starter-project\\Simulator\\images\\photos**, and pick the photo of either the man or woman to upload, and select Open.

    ![Screenshot of Open window, expanded to the photos folder where two images display: female.jpeg, and male.jpeg.](./media/file-explorer-photos.png "Photos folder")

9. In a few moments, you should see the price change from $1.25 to whatever value the predictive model suggested.

    ![The Vending Machine Simulator now displays the coconut water price as $0.91.](./media/vending-macine-simulator-price-change.png "Vending Machine Simulator")

10. Try using the other photo or your own photo to see what prices are suggested.

11. Click the X at the top right of the application to stop it.

## Exercise 3: Implement purchasing

Duration: 15 minutes

In this exercise, you will create an in-memory, columnar index table in SQL DB that will be used to support purchase transactions in a real-time analytics fashion, and then implement the purchasing process in the vending machine simulator. Finally, you will run the simulator and purchase items.

### Task 1: Create the transactions table

1. Within Visual Studio Solution Explorer, expand the SQL Scripts folder and open the file `Create Table.sql`.

    ![Screenshot of the Visual Studio Solution Explorer expanded to Create Table.sql.](./media/visual-studio-solution-explorer-sql-scripts-create-table.png "Visual Studio Solution Explorer")

2. Replace `TODO 1` with the following:

    ```sql
    --TODO: 1. Transaction ID should be a Primary Key, fields with a b-tree index
    TransactionId int IDENTITY NOT NULL PRIMARY KEY NONCLUSTERED,
    ```

3. Replace `TODO 2` with the following:

    ```sql
    --TODO: 2. This table should have a columnar index
    INDEX Transactions_CCI CLUSTERED COLUMNSTORE
    ```

4. Replace `TODO 3` with the following:

    ```sql
    --TODO: 3. This should be an in-memory table
    MEMORY_OPTIMIZED = ON
    ```

5. Replace `TODO 4` with the following:

    ```sql
    --TODO: 4. In-memory tables should auto-elevate their transaction level to Snapshot
    ALTER DATABASE CURRENT SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON;
    ```

6. Save the script.

7. Execute the script by pressing the play icon.

    ![Visual Studio execute SQL script button](./media/visual-studio-execute-sql.png "Visual Studio execute SQL script button")

8. Expand **Azure**, and if prompted, sign in with your Azure credentials.

9. From the Azure node, and select the database you created for the vending database.

    ![Screenshot of the Connect window. Azure is expanded, and vending (vendingmachineslab) is highlighted.](./media/visual-studio-database-connect.png "Visual Studio database connect window")

10. In the in fields at the bottom, enter your user name and password for the SQL Server, and select Connect. The script should run successfully.

    ![Screenshot of the Query executed successfully message.](./media/visual-studio-execute-sql-success.png "Query executed successfully message")

### Task 2: Configure the Simulator

1. In the [Azure portal](https://portal.azure.com), navigate to the SQL database and select the Show database connection strings near the top of the Overview blade.

    ![Screenshot of the Azure Portal, Database section. Under Connection strings, the link to "Show database connection strings" is highlighted.](./media/sql-database-show-connection-strings.png "Show SQL Database Connection Strings")

2. Copy the connection string on the ADO.NET tab of the Database connection string blade, and paste the value into a text editor, such as Notepad, for later reference.

    ![Screenshot of the ADO.NET tab, with the connection string selected, and the copy button highlighted.](./media/sql-database-ado-net-connection-string.png "SQL Database ADO.NET connection string")

3. In the Simulator project, open `App.config`.

4. Within the `connectionString` section, set the following:

    * **TransactionsModel**: set the value of the connectionString attribute to the ADO.NET connection string to your SQL DB instance. **Do not forget to replace the values for {your_username} and {your_password} with your actual credentials.**

        * **User name**: demouser

        * **Password**: Password.1!!

5. Save the `App.config`.

    ![The App.config file is shown in Visual Studio with the updated connection string to the database.](./media/visual-studio-app-config-connectionstrings.png "Connection Strings value")

### Task 3: Test purchasing

1. In solution explorer, right-click the Simulator project, and select Build. Note: You may need to ensure the previous instance you started has been closed before rebuilding.

2. Ensure that your build generates no errors.

3. From the Debug menu, select **Start Without Debugging**.

4. In the Simulator, select buy.

    ![The Vending Machine Simulator displays the ad for coconut water for \$1.25, with a buy button.](./media/simulator-app-coconut-water.png "Vending machine simulation")

5. You should see a confirmation dialog similar to the following:

    ![Screenshot of the Purchase Complete dialog box, with the message "Enjoy!"](./media/vending-machine-simulator-buy.png "Purchase Complete dialog box")

## Exercise 4: Implement device command and control

Duration: 45 minutes

In this exercise, you will implement the ability to push new promotions to the vending machine simulator using the command and control features of IoT Hub. You will update the simulator to listen for these messages. You will also update the console application DeviceControlConsole to send selected promotions.

### Task 1: Provision IoT Hub

In these steps, you will provision an instance of IoT Hub.

1. In your browser, navigate to the [Azure portal](https://portal.azure.com), select **+Create a resource** in the navigation pane, enter "iot" into the Search the Marketplace box, select **IoT Hub** from the results, and select **Create**.

    ![+Create a resource is highlighted in the navigation page of the Azure portal, and "iot" is entered into the Search the Marketplace box. IoT Hub is highlighted in the search results.](./media/create-resource-iot-hub.png "Create an IoT Hub")

2. On the IoT Hub blade Basics tab, enter the following:

    * **Subscription**: Select the subscription you are using for this hands-on lab

    * **Resource group**: Choose Use existing and select the hands-on-lab-SUFFIX resource group

    * **Region**: Select the location you are using for this hands-on lab

    * **IoT Hub Name**: Enter a unique name, such as vendingmachineshubSUFFIX

        ![The Basics blade for IoT Hub is displaye, with the values specified above entered into the appropriate fields.](./media/iot-hub-basics-blade.png "Create IoT Hub Basic blade")

    * Select **Next: Size and Scale**.

    * On the Size and scale blade, accept the default Pricing and scale tier of S1: Standard tier, and select **Review + create**.

    * Select **Create** on the Review + create blade.

3. When the IoT Hub deployment is completed, you will receive a notification in the Azure portal. Select **Go to resource** in the notification.

    ![Screenshot of the Deployment succeeded message, with the Go to resource button highlighted.](./media/iot-hub-deployment-succeeded.png "Deployment succeeded message")

4. From the IoT Hub's Overview blade, select **Shared access policies** under Settings on the left-hand menu.

    ![Screenshot of the Overview blade, settings section. Under Settings, Shared access policies is highlighted.](./media/iot-hub-shared-access-policies.png "Overview blade, settings section")

5. Select **iothubowner** policy.

    ![The Azure portal is shown with the iothubowner selected.](./media/iot-hub-shared-access-policies-iothubowner.png "IoT Hub Owner shared access policy")

6. In the **iothubowner** blade, select the Copy button to the right of the Connection string - primary key field. Paste the connection string value into a text editor, such as Notepad, as this will be needed later in this lab.

    ![Screenshot of the iothubowner blade. A callout points to the copy button to the right of the connection string - primary key field.](./media/iot-hub-shared-access-policies-iothubowner-blade.png "iothubowner blade")

### Task 2: Listen for control messages

1. Within Visual Studio Solution Explorer, expand the Simulator project, and open the file `MainWindow.xaml.cs`.

2. Scroll down to the `ListenForControlMessages` method.

    ![In the Visual Studio Solution Explorer window, `ListenForControlMessages` is highlighted.](./media/visual-studio-mainwindowxaml-listenforcontrolmessages.png "Visual Studio Solution Explorer")

3. Uncomment the body of the while(true) loop. You can uncomment a block of code by selecting the code, then selecting the Uncomment button on the toolbar.

    ![Screenshot of the Solution Explorer toolbar, with the Uncomment button highlighted.](./media/visual-studio-toolbar-uncomment.png "Solution Explorer toolbar")

4. Replace `TODO 1` with the following:

    ```csharp
    //TODO: 1. Receive messages intended for the device via the instance of _deviceClient.
    Microsoft.Azure.Devices.Client.Message receivedMessage = await _deviceClient.ReceiveAsync();
    ```

5. Replace `TODO 2` with the following:

    ```csharp
    //TODO: 2. A null message may be received if the wait period expired, so ignore and call the receive operation again
    if (receivedMessage == null) continue;
    ```

6. Replace `TODO 3` with the following:

    ```csharp
    //TODO: 3. Deserialize the received binary encoded JSON message into an instance of PromoPackage.
    string receivedJSON = Encoding.ASCII.GetString(receivedMessage.GetBytes());
    System.Diagnostics.Trace.TraceInformation("Received message: {0}", receivedJSON);
    PromoPackage promo = Newtonsoft.Json.JsonConvert.DeserializeObject<PromoPackage>(receivedJSON);
    ```

7. Replace `TODO 4` with the following:

    ```csharp
    //TODO: 4. Acknowledge receipt of the message with IoT Hub
    await _deviceClient.CompleteAsync(receivedMessage);
    ```

8. Save the file.

### Task 3: Send control messages

1. Within Visual Studio Solution Explorer, expand the **DeviceControlConsole** project, and open the file `Program.cs`.

    ![Screenshot of Visual Studio Solution Explorer, with DeviceControlConsole expanded, and Program.cs highlighted.](./media/visual-studio-solution-explorer-devicecontrolconsole-program-cs.png "Visual Studio Solution Explorer")

2. Scroll down to the PushPromo method.

    ![PushPromo method](./media/visual-studio-program-pushpromo.png "Visual Studio Solution Explorer")

3. Replace `TODO 1` with the following:

    ```csharp
    //TODO: 1. Create a Service Client instance provided the _IoTHubConnectionString
    _serviceClient = ServiceClient.CreateFromConnectionString(_IoTHubConnectionString);
    ```

4. Replace `TODO 2` with the following:

    ```csharp
    //TODO: 2. Send the command
    await _serviceClient.SendAsync(deviceId, commandMessage);
    ```

5. Save `Program.cs`.

### Task 4: Configure the DeviceControlConsole and Simulator

1. In **DeviceControlConsole**, open `App.config`.

    ![Screenshot of Visual Studio Solution Explorer, with DeviceControlConsole expanded, and App.config highlighted.](./media/visual-studio-solution-explorer-devicecontrolconsole-app-config.png "Visual Studio Solution Explorer")

2. Set the `IoTHubConnectionString` appSetting to have a value of the connection string for the service policy to your IoT Hub. (Recall you can get this from the Azure Portal IoT Hub blade, Shared access policies, and then select the policy.)

    ![Screenshot of the iothubowner blade. A callout points to the copy button to the right of the connection string - primary key field.](./media/iot-hub-shared-access-policies-iothubowner-blade.png "iothubowner blade")

3. Set the `storageConnectionString` appSetting to have the same connection string for your storage account that the `App.config` file in the Simulator project has.

4. Save the file.

5. Now, open the `App.config` file in the Simulator project.

6. Set the `IoTHubSenderConnectionString` appSetting to have a value of the connection string for the device policy to your IoT Hub.

7. Set the `IoTHubManagerConnectionString` appSetting to have a value of the connection string for the **iothubowner** policy to your IoT Hub.

8. Save the file.

9. Build the Simulator and DeviceControlConsole projects.

10. In Solution Explorer, right-click Solution 'Vending Machines,' and select Set StartUp Projects.

    ![Screenshot of the Solution Explorer sub-menu for Solution VendingMachines (3 projects). Set StartUp Projects is selected.](./media/visual-studio-solution-explorer-set-startup-projects.png "Solution Explorer")

11. In the dialog, select the Multiple startup projects option, and ensure that Action is set to Start for both DeviceControlConsole and Simulator.

    ![Screenshot of the VendingMachines solution Property Pages Dialog Box.](./media/visual-studio-solution-explorer-select-startup-projects.png "VendingMachines Property Pages Dialog Box")

12. Select OK.

13. From the Debug menu, choose Start without Debugging.

14. Wait for both the Vending Machine Simulator and the DeviceControlConsole to appear.

    ![Screenshot of the Vending Machine Simulator and DeviceControlConsole. The Vending Machine Simulator displays the coconut water ad.](./media/vending-machine-simulator-plus-devicecontrolconsole.png "Vending Machine Simulator and DeviceControlConsole")

15. In the DeviceControlConsole, press 1 to push the promotion for Soda.

    ![Screenshot of the Vending Machine Simulator and DeviceControlConsole. This time, the Vending Machine Simulator displays the soda ad.](./media/vending-machine-simulator-plus-devicecontrolconsole-select.png "Vending Machine Simulator and DeviceControlConsole")

16. Observe that the entire promotion surface of the vending machine changes (product name, price, and image).

    > **Note:** If the photo does not change, and after a few minutes you receive a DotNetty.Transport... error, you will need to delete and recreate your IoT Hub in the Azure portal. The error is caused by a communication error between the application and your IoT Hub. Be sure to update your App.config file with the new IoT hub connection strings.

17. Experiment sending the other promotion or toggling between promotions.

18. Experiment with making purchases and sending photos to verify the other functions still work with the new promoted products.

## Exercise 5: Analytics with Power BI Desktop

Duration: 15 minutes

In this exercise, you will use Power BI Desktop to query purchase data from the in-memory table of SQL DB and visualize the result.

### Task 1: Build the query and create the visualization

1. From your Start menu on your Lab VM, open Power BI Desktop.

    ![The Power BI Desktop link is shown.](./media/power-bi-desktop-start-menu-tile.png "Power BI Desktop Start menu tile")

2. In the opening dialog, select **Get Data**.

    ![Screenshot of the Power BI Desktop opening dialog box, with the Get Data link.](./media/power-bi-desktop-get-data.png "Power BI Desktop opening dialog box")

3. In the Get Data dialog, select **Azure** in the categories list and then **Azure SQL Database**.

    ![Screenshot of the Get Data dialog box.](./media/power-bi-desktop-get-data-azure-sql-database.png "Get Data dialog box")

4. Select Connect.

5. In the dialog, enter the name of your SQL Server (e.g., myserver.database.windows.net), the name of your vending database, and select the **DirectQuery** option. Select OK.

    ![Screenshot of the SQL Server Database dialog box.](./media/power-bi-desktop-get-data-azure-sql-database-properties.png "SQL Server Database dialog box")

6. On the next screen, select the **Database** tab on the left, and provide your SQL username (demouser) and password (Password.1!!), then select **Connect**.

    ![Screenshot of the SQL Server Database dialog box.](./media/power-bi-desktop-get-data-azure-sql-database-connect.png "SQL Server database dialog box")

7. In the Navigator dialog, check the box next to **Transactions**.

    ![Screenshot of the Navigator dialog box. In the left pane, under Display options, the checkbox is selected for Transactions.](./media/power-bi-desktop-get-data-azure-sql-database-select-tables.png "Navigator dialog box")

8. Select **Load**.

9. In the Ribbon, select **Edit Queries**.

    ![The Power BI Desktop Edit Queries button is shown.](./media/power-bi-desktop-edit-queries-button.png "Edit Queries button")

10. In the Query Editor, select the Transaction Date column header to select the column.

    ![The TransactionDate column is shown.](./media/power-bi-desktop-edit-queries-transactiondata.png "TransactionData column")

11. In the Ribbon, select the **Add Column** tab and select Time, Hour, Hour.

    ![Screenshot of the Query Editor. On the ribbon, the Add Column tab is selected. On the Add Column ribbon, Time is selected. From its sub-menu, Hour is selected, and from Hour's sub-menu, Hour is selected again.](./media/power-bi-desktop-edit-queries-add-column.png "Query Editor")

12. Select the **TransactionDate** column again.

13. In the Ribbon, select **Time, Minute.**

    ![On the Add Column ribbon, Time is selected, and from its sub-menu, Minute is selected.](./media/power-bi-desktop-edit-queries-add-column-time.png "Time sub-menu")

14. Select the **TransactionDate** one more time.

15. In the Ribbon, select **Time, Second**.

16. In the Ribbon, on the Home tab, select **Close & Apply**.

    ![Close and Apply is shown.](./media/power-bi-desktop-edit-queries-close-apply.png)

17. In the message that appears, select **Apply Changes**.

    ![The Apply Changes message displays, informing you that there are pending changes in your queries that haven't been applied.](./media/power-bi-desktop-edit-queries-apply-changes.png "Apply Changes message")

18. In the Visualizations, select **Stacked column chart**.

    ![Screenshot of the Visualizations menu, with the stacked column chart icon selected.](./media/power-bi-desktop-visualizations-stacked-column-chart.png "Visualizations menu")

19. From the Fields list, drag the **minute** field over to the **axis** property.

    ![Minute has been added under Axis.](./media/power-bi-desktop-visualizations-stacked-column-chart-axis.png "Stack Column Chart Axis")

20. From the Fields list, drag the **PurchasePrice** over to the **value** property.

    ![The PurchasePrice has been added under Value.](./media/power-bi-desktop-visualizations-stacked-column-chart-value.png "Stack Column Chart Value")

21. Your completed visualization summarizing the most profitable minutes in each hour should appear as follows:

    ![Screenshot of a stacked column chart detailing the purchase price by minute. At this time, we are unable to capture all of the information in the chart. Future versions of this course should address this.](./media/power-bi-desktop-visualizations-stacked-column-chart-tile.png "stacked column chart")

## Congratulations, you have completed the Intelligent Vending Machines Hands-on Lab

Please proceed to the [After the hands-on lab](./clean-up.md) section to ensure the Azure resources provisioned for this lab are deleted, and you do not continue to incur charges for those resources.