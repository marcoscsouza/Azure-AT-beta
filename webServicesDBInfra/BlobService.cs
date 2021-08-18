using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;
using webServicesDBInfra.Interfaces;

namespace webServicesDBInfra
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string _containerName = "azureimageteste";

        public BlobService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<string> UploadAsync(Stream stream)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            await container.CreateIfNotExistsAsync(); //idempotencia
            await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            var blobClient = container.GetBlobClient($"{Guid.NewGuid()}.jpg");

            await blobClient.UploadAsync(stream);
            return blobClient.Uri.ToString();

        }

        public async Task DeleteAsync(string blobName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobClient = container.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
