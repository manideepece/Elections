using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elections.Models
{
    public class Utility
    {
        public static void CreateRequestResponseFilesInBlob(string message, string fileName)
        {
            // Create a BlobServiceClient object which will be used to create a container client

            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=m4b8storage;AccountKey=wYCMUtFZv3TuqjmkntcrGQpOPX+qtQuK98aSNNb8I9uwIiRG+a+5fM8YFudu6w9EJ9a1eZlUMjaDRegaD+tr6A==;EndpointSuffix=core.windows.net");
            
            //Create a unique name for the container
            string containerName = "m4b8-elections-request-response";

            try
            {
                // Create the container and return a container client object
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

                using (var stream = GetStreamFromString(message))
                {
                    blobClient.Upload(stream);
                    stream.Close();
                }
            }
            catch(Exception ex)
            {
                BlobContainerClient containerClient = blobServiceClient.CreateBlobContainer(containerName);

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

                using (var stream = GetStreamFromString(message))
                {
                    blobClient.Upload(stream);
                    stream.Close();
                }
            }
        }

        private static Stream GetStreamFromString(string message)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(message);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
    }
}
