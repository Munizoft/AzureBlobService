using Newtonsoft.Json;
using System;
using System.IO;

namespace Munizoft.Azure.Services.Models
{
    public class BlobInfo
    {
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public Stream Content { get; set; }

        [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
        public String ContentType { get; set; }

        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public String Filename { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public String URL { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public Int64 Size { get; set; }

        [JsonProperty("created_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("last_modified_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastModifiedOn { get; set; }

        public BlobInfo()
        {            
        }

        public BlobInfo(String filename, String url)
        {
            Filename = filename;
            URL = url;
        }

        public BlobInfo(Stream content, String contentType, String filename, String url)
        {
            Filename = filename;
            URL = url;

            Content = content;
            Size = content.Length;
            ContentType = contentType;
        }
    }
}