using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Munizoft.Azure.Services.Extensions;
using Munizoft.Azure.Services.Models;
using Munizoft.Azure.Services.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Munizoft.Azure.Services
{
    public class BlobService : IBlobService
    {
        #region Fields
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AzureBlobServicesOptions _options;
        private readonly IMapper _mapper;
        #endregion Fields

        #region Constructor
        public BlobService(
            BlobServiceClient blobServiceClient, 
            IOptions<AzureBlobServicesOptions> options,
            IMapper mapper)
        {
            _blobServiceClient = blobServiceClient;
            _options = options.Value;
            _mapper = mapper;
        }
        #endregion Constructor

        /// <summary>
        ///     Get Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Models.BlobInfo> GetBlobAsync(String containerName, String name)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);            
            var blobClient = container.GetBlobClient(HttpUtility.UrlDecode(name));

            return this._mapper.Map<BlobClient, Models.BlobInfo>(blobClient);
        }

        /// <summary>
        ///     Download Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Stream> DownloadAsync(String containerName, String name)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(HttpUtility.UrlDecode(name));
            var blobDownloadInfo = await blobClient.DownloadAsync();

            return blobDownloadInfo.Value.Content;
        }

        /// <summary>
        ///     List Blobs
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<String>> ListBlobsAsync(String containerName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var items = new List<String>();

            await foreach (var item in container.GetBlobsAsync())
            {
                items.Add(item.Name);
            }

            return items;
        }

        /// <summary>
        ///     Upload File
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="filePath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task UploadFileAsync(String containerName, String filePath, String filename)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var client = container.GetBlobClient(filename);
            await client.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });
        }

        /// <summary>
        ///     Upload Content
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task UploadContentAsync(UploadContentRequest request)
        {
            var container = _blobServiceClient.GetBlobContainerClient(request.Container);
            var client = container.GetBlobClient(request.Filename);
            var bytes = Encoding.UTF8.GetBytes(request.Content);
            await using var memoryStream = new MemoryStream(bytes);

            await client.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = request.Content.GetContentType() });
        }

        /// <summary>
        ///     Delete Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public async Task DeleteBlobAsync(String containerName, String blobName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var client = container.GetBlobClient(blobName);
            await client.DeleteAsync();
        }
    }
}