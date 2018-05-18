using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Mowards.MowardsService
{
    public class AzureStorageService
    {
        public static async Task<string> LoadImage(System.IO.Stream imageFile,  string imageFilePath)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = 
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=mowardsstorage;AccountKey=59nNYHJNMuF/O9WEZPVT0XrWMofTVgr2BEjm4E+imVCNes7pwlbX9b5wp+ZOCxGQwUQf9rdNScda3jjv5laP5A==;EndpointSuffix=core.windows.net");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(Utils.FILE_CONTAINER_NAME);

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            string blobName = GetProfilePictureName(imageFilePath);

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            await blockBlob.UploadFromStreamAsync(imageFile);

            return blockBlob.Uri.ToString();
        }

        public static string GetProfilePictureName(string imageFilePath)
        {
            return System.Guid.NewGuid().ToString() + "-pic" + System.IO.Path.GetExtension(imageFilePath);
        }

    }
}
