using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SDX.FunctionsDemo.FunctionApp.Utils
{
    public static class StorageHelper
    {
        private static CloudStorageAccount _cloudStorageAccount;

        public static CloudStorageAccount GetStorageAccount(this IConfiguration configuration)
        {
            if (_cloudStorageAccount != null)
                return _cloudStorageAccount;

            var connectionString = configuration[StorageDefines.StorageConnectionString];
            if (CloudStorageAccount.TryParse(connectionString, out _cloudStorageAccount))
                return _cloudStorageAccount;

            throw new InvalidOperationException("Connection-String " + StorageDefines.StorageConnectionString + " ungültig!");
        }

        public static CloudQueue GetQueue(this CloudStorageAccount cloudStorageAccount, string queueName)
        {
            var queueClient = cloudStorageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(queueName);
            return queue;
        }

        public static async Task AddMessageAsync(this CloudQueue queue, string content)
        {
            await queue.CreateIfNotExistsAsync();
            var msg = new CloudQueueMessage(content);
            await queue.AddMessageAsync(msg);
        }

        public static CloudBlobContainer GetBlobContainer(this CloudStorageAccount cloudStorageAccount, string containerName)
        {
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(containerName);
            return blobContainer;
        }

        public static async Task UploadBlobAsync(this CloudBlobContainer blobContainer, string blobName, byte[] data)
        {
            await blobContainer.CreateIfNotExistsAsync();
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);
        }

        public static async Task<IEnumerable<CloudBlob>> FindBlobsStartingWithAsync(this CloudBlobContainer container, string blobNamePrefix)
        {
            var blobResultSegment = await container.ListBlobsSegmentedAsync(blobNamePrefix, null);
            var result = blobResultSegment.Results.Cast<CloudBlob>();
            return result;
        }
    }
}
