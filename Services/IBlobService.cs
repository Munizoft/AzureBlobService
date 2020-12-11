using Munizoft.Azure.Services.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Munizoft.Azure.Services
{
    public interface IBlobService
    {
        /// <summary>
        ///     Get Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Models.BlobInfo> GetBlobAsync(String containerName, String name);

        /// <summary>
        ///     Download Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Stream> DownloadAsync(String containerName, String name);

        /// <summary>
        ///     List Blobs
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<IEnumerable<String>> ListBlobsAsync(String containerName);

        /// <summary>
        ///     Upload File
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="filePath"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task UploadFileAsync(String containerName, String filePath, String filename);

        /// <summary>
        ///     Upload Content
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task UploadContentAsync(UploadContentRequest request);

        /// <summary>
        ///     Delete Blob
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task DeleteBlobAsync(String containerName, String blobName);
    }
}