using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DeviceControlConsole
{
    class Program
    {
        static string _storageConnectionString = System.Configuration.ConfigurationManager.AppSettings["storageConnectionString"];
        static string _IoTHubConnectionString = System.Configuration.ConfigurationManager.AppSettings["IoTHubServiceConnectionsString"];
        static ServiceClient _serviceClient; 

        static void Main(string[] args)
        {
            string deviceId = "112358";
            bool keepLooping = true;

            while (keepLooping)
            {
                Console.WriteLine("Which promo would you like to push out?");
                Console.WriteLine("1. Soda");
                Console.WriteLine("2. Water");
                Console.WriteLine("Press any other key to quit.");

                char selection = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (selection == '1' || selection == '2')
                {
                    PromoPackage promoPackage = CreatePromoPackage(selection);

                    PushPromo(deviceId, promoPackage).Wait();

                    Console.WriteLine("Command sent");
                }

                else
                {
                    keepLooping = false;
                }
            }
        }

        static PromoPackage CreatePromoPackage(char selection)
        {
            PromoPackage promoPackage = null;
            if (selection == '1')
            {
                promoPackage = new PromoPackage()
                {
                    ImageUri = GetUriForPromoImage("Soda.png"),
                    Price = 0.99,
                    ProductId = 1,
                    ProductTitle = "soda"
                };
            }
            else
            {
                promoPackage = new PromoPackage()
                {
                    ImageUri = GetUriForPromoImage("Water.png"),
                    Price = 0.75,
                    ProductId = 2,
                    ProductTitle = "water"
                };
            }

            return promoPackage;
        }

        static string GetUriForPromoImage(string imageName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("promo");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);

            return GetBlobSasUri(blockBlob);
        }

        static async Task PushPromo(string deviceId, PromoPackage promoPackage)
        {
            //TODO: 1. Create a Service Client instance provided the _IoTHubConnectionString
            _serviceClient = /*Complete this*/ ;

            var promoPackageJson = JsonConvert.SerializeObject(promoPackage);

            Console.WriteLine("Sending Promo Package:");
            Console.WriteLine(promoPackageJson);

            var commandMessage = new Message(Encoding.ASCII.GetBytes(promoPackageJson));

            //TODO: 2. Send the command
            await _serviceClient./*Complete this*/ ;
        }

        static string GetBlobSasUri(CloudBlockBlob blob)
        {
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write;

            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            return blob.Uri + sasBlobToken;
        }

        class PromoPackage
        {
            public string ImageUri;
            public string ProductTitle;
            public int ProductId;
            public double Price;
        }
    }
}
