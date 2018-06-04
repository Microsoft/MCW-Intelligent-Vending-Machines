using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string _storageConnectionString = System.Configuration.ConfigurationManager.AppSettings["storageConnectionString"];
        string _faceApiKey = System.Configuration.ConfigurationManager.AppSettings["faceAPIKey"];
        private string _faceEndpoint = System.Configuration.ConfigurationManager.AppSettings["faceEndpoint"];

        string _vendingMachineId = "112358";
        string _itemName = "coconut water";
        double _purchasePrice = 1.25;
        int _itemId = 0;
        string _imagePath = System.IO.Path.GetFullPath("images/CoconutWater.png");

        DeviceClient _deviceClient;
        private RegistryManager _registryManager;
        private string _iotHubSenderConnectionString;
        private string _iotHubManagerConnectionString;
        private string _deviceKey;

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private async void btnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;             
                await UpdateDynamicPrice(filename);
            }
        }


        private async Task UpdateDynamicPrice(string filename)
        {
            // TODO 1. Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = /*Complete this*/ 
            CloudBlobClient blobClient = storageAccount./*Complete this*/ ;
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            // TODO 2. Retrieve reference to a blob named with the value of fileName.
            string blobName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(filename);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference( /*Complete this*/ );

            // TODO 3. Create or overwrite the blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(filename))
            {
                blockBlob./*Complete this*/;
            }

            // Acquire a SAS Uri for the blob
            string sasUri = GetBlobSasUri(blockBlob);

            // Provide the SAS Uri to blob to the Face API
            Demographics d = await GetPhotoDemographics(sasUri);

            double suggestedPrice = 1.10;

            //TODO 10. Invoke the actual ML Model
            //PricingModelService pricingModel = new PricingModelService();
            //string gender = d.gender == "Female" ? "F" : "M";
            //suggestedPrice = ...

            SetPromo(_itemName, suggestedPrice, _itemId, _imagePath);
        }

        private void SetPromo(string title, double price, int productId, string imagePath)
        {
            textPromoTitle.Text = title;
            textPromoPrice.Text = string.Format("{0:c}", price);
            ImageSource imageSource = new BitmapImage(new Uri(imagePath));
            promotedImage.Source = imageSource;

            _purchasePrice = price;
            _itemName = title;
            _itemId = productId;
            _imagePath = imagePath;
        }

        class Demographics
        {
            public double age;
            public string gender;
        }

        private async Task<Demographics> GetPhotoDemographics(string sasUri)
        {
            Demographics d = null;

            //TODO 6. Invoke Face API with URI to photo
            IFaceServiceClient faceServiceClient = /*Complete this*/;

            //TODO 7. Configure the desired attributes Age and Gender
            IEnumerable<FaceAttributeType> desiredAttributes = new FaceAttributeType[] { /*Complete this*/ , /*Complete this*/  };

            //TODO 8. Invoke the Face API Detect operation
            Face[] faces = await faceServiceClient./*Complete this*/(sasUri, false, true, desiredAttributes);

            if (faces.Length > 0)
            {
                //TODO 9. Extract the age and gender from the Face API response
                double computedAge = faces[0].FaceAttributes./*Complete this*/ ;
                string computedGender = faces[0].FaceAttributes./*Complete this*/ ;

                d = new Demographics()
                {
                    age = faces[0].FaceAttributes.Age,
                    gender = faces[0].FaceAttributes.Gender
                };
            }

            return d;
        }

        static string GetBlobSasUri(CloudBlockBlob blob)
        {
            //TODO: 4. Create a Read blob and Write blob Shared Access Policy that is effective 5 minutes ago and for 2 hours into the future
            SharedAccessBlobPolicy sasConstraints = /*Complete this*/ ;
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(/*Complete this*/ );
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(/*Complete this*/ );
            sasConstraints.Permissions = SharedAccessBlobPermissions./*Complete this*/  | SharedAccessBlobPermissions./*Complete this*/ ;

            //TODO: 5. Construct the full URI with SAS
            string sasBlobToken = blob.GetSharedAccessSignature(/*Complete this*/ );
            return blob.Uri + sasBlobToken;
        }

        private async void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            bool result = await CompletePurchaseAsync();
            if (result)
            {
                MessageBox.Show("Enjoy!", "Purchase Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Unable to Complete Purchase", "Oh no!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void TrackPurchaseEvent(Transaction t)
        {
            try
            {
                string transactionJSON = Newtonsoft.Json.JsonConvert.SerializeObject(t);
                TransmitEvent(transactionJSON);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }
        }

        private async Task<bool> CompletePurchaseAsync()
        {
            bool success = false;
            try
            {
                TransactionsModel model = new TransactionsModel();

                Transaction t = new Transaction()
                {
                    ItemName = _itemName,
                    PurchasePrice = (decimal)_purchasePrice,
                    TransactionDate = DateTime.UtcNow,
                    TransactionStatus = 2,
                    VendingMachineId = _vendingMachineId,
                    ItemId = _itemId
                };
                model.Transactions.Add(t);
                
                await model.SaveChangesAsync();

                success = true;

                TrackPurchaseEvent(t);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }

            return success;
        }

        public async void Init()
        {
            try
            {
                _iotHubSenderConnectionString = System.Configuration.ConfigurationManager.AppSettings["IoTHubSenderConnectionsString"];
                _iotHubManagerConnectionString = System.Configuration.ConfigurationManager.AppSettings["IoTHubManagerConnectionsString"];
                _registryManager = RegistryManager.CreateFromConnectionString(_iotHubManagerConnectionString);

                await RegisterDeviceAsync();

                InitDeviceClient();

                ListenForControlMessages();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }

        }

        void InitDeviceClient()
        {
            var builder = Microsoft.Azure.Devices.IotHubConnectionStringBuilder.Create(_iotHubSenderConnectionString);
            string iotHubName = builder.HostName;

            _deviceClient = DeviceClient.Create(iotHubName,
                new DeviceAuthenticationWithRegistrySymmetricKey(_vendingMachineId, _deviceKey), 
                Microsoft.Azure.Devices.Client.TransportType.Mqtt);

            
        }

        async Task RegisterDeviceAsync()
        {
            Device device = new Device(_vendingMachineId);
            device.Status = DeviceStatus.Disabled;

            try
            {
                device = await _registryManager.AddDeviceAsync(device);
            }
            catch (Microsoft.Azure.Devices.Common.Exceptions.DeviceAlreadyExistsException)
            {
                //Device already exists, get the registered device
                device = await _registryManager.GetDeviceAsync(_vendingMachineId);
            }

            try
            {
                //Ensure device is activated
                device.Status = DeviceStatus.Enabled;
                await _registryManager.UpdateDeviceAsync(device);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }

            _deviceKey = device.Authentication.SymmetricKey.PrimaryKey;
        }

        void TransmitEvent(string datapoint)
        {
            Microsoft.Azure.Devices.Client.Message message;
            try
            {
                message = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(datapoint));

                _deviceClient.SendEventAsync(message);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }
        }

        class PromoPackage
        {
            public string ImageUri;
            public string ProductTitle;
            public int ProductId;
            public double Price;
        }

        private async void ListenForControlMessages()
        {
            Task.Delay(3000).Wait();

            //while (true)
            //{
            //    //TODO: 1. Receive messages intended for the device via the instance of _deviceClient.
            //    Microsoft.Azure.Devices.Client.Message receivedMessage = await _deviceClient./*Complete this*/ ;

            //    //TODO: 2. A null message may be received if the wait period expired, so ignore and call the receive operation again
            //    if (receivedMessage == null) continue;

            //    //TODO: 3. Deserialize the received binary encoded JSON message into an instance of PromoPackage.
            //    string receivedJSON = Encoding.ASCII.GetString(receivedMessage.GetBytes());
            //    System.Diagnostics.Trace.TraceInformation("Received message: {0}", receivedJSON);
            //    PromoPackage promo = Newtonsoft.Json.JsonConvert./*Complete this*/(receivedJSON);

            //    ApplyPromoPackageAsync(promo);

            //    //TODO: 4. Acknowledge receipt of the message with IoT Hub
            //    await _deviceClient./*Complete this*/(receivedMessage);
            //}
        }


        private async void ApplyPromoPackageAsync(PromoPackage promo)
        {
            CloudBlockBlob blob = new CloudBlockBlob(new Uri(promo.ImageUri));
            string path = System.IO.Path.GetFullPath(String.Format("{0}-{1}.png", promo.ProductId, promo.ProductTitle));
            if (!File.Exists(path))
            {
                await blob.DownloadToFileAsync(path, FileMode.Create);
            }
            
            SetPromo(promo.ProductTitle, promo.Price, promo.ProductId, path);

        }
    }
}
