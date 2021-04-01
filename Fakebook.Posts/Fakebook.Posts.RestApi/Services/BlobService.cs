using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Fakebook.Posts.RestApi.Services
{
    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// Uploads a blob, given a name, type, and filename.
        /// </summary>
        /// <param name="blobContainerName">Container name used for blob storage</param>
        /// <param name="content">What is being stored</param>
        /// <param name="contentType">Content type of what's being uploaded</param>
        /// <param name="fileName">Name of file in blob storage</param>
        /// <returns>Blob Client URI</returns>
        public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });

            return blobClient.Uri;
        }

        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

            containerClient.CreateIfNotExists(PublicAccessType.Blob);

            return containerClient;
        }
    }
}