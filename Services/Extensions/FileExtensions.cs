using Azure.Storage.Blobs;
using Microsoft.AspNetCore.StaticFiles;
using System;

namespace Munizoft.Azure.Services.Extensions
{
    public static class FileExtensions
    {
        private static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();
        public static String GetContentType(this String filename)
        {
            if (!Provider.TryGetContentType(filename, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        public static String BlobUrl(this BlobClient blob)
        {
            return blob.Uri.AbsoluteUri;
        }

        public static Models.BlobInfo ToBlobInfo(this BlobClient blobClient)
        {
            var properties = blobClient.GetProperties();
            return new Models.BlobInfo(blobClient.Name, blobClient.Uri.AbsoluteUri);
        }
    }
}