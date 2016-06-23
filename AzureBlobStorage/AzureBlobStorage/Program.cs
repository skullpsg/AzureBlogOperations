using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace AzureBlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {

            string FilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\ZippedFile\Zip_File\Web_host.zip";

            // Retrieve storage account from connection string.
            string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");


            CloudBlobContainer container = CrateContainer(connectionString).Result;

            ListingBlobs(container);
            //this function is used to upload a zip file from our project to Azure Blob
            var _completedStatus = Upload(container, FilePath);

            //this function is used to read a zip file from  Azure Blob and create 
            var _downloadStreat = Download(container);

            ListingBlobs(container);

            Console.ReadKey();

        }
        public static async Task<CloudBlobContainer> CrateContainer(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("samplecontainer");

            // Create the container if it doesn't already exist.
            if (await container.CreateIfNotExistsAsync())
            {
                await container.SetPermissionsAsync(
                 new BlobContainerPermissions
                 {
                     PublicAccess =
                         BlobContainerPublicAccessType.Blob
                 });
            }
            return container;
        }

        public static bool Upload(CloudBlobContainer container, string filePath)
        {

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("fileUpload");

            var fileStream = File.ReadAllBytes(filePath);
            blockBlob.Properties.ContentType = "application/zip";
            blockBlob.UploadFromByteArray(fileStream, 0, fileStream.Length);
            return true;
        }
        private static bool ListingBlobs(CloudBlobContainer container)
        {
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
            return true;
        }

        public static bool Download(CloudBlobContainer container)
        {
            string FilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\ZippedFile\DownloadFile\Web_host.zip";

            CloudBlockBlob blob = container.GetBlockBlobReference("fileUpload");
            blob.FetchAttributes();
            long fileByteLength = blob.Properties.Length;
            Byte[] myByteArray = new Byte[fileByteLength];
            blob.DownloadToByteArray(myByteArray, 0);
            File.WriteAllBytes(FilePath, myByteArray);
            return true;
        }


    }

}
