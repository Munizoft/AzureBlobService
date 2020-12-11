using Newtonsoft.Json;
using System;

namespace Munizoft.Azure.Services.Resources
{
    public class UploadFileRequest
    {
        [JsonProperty("container")]
        public String Container { get; set; }

        [JsonProperty("filePath")]
        public String FilePath { get; set; }

        [JsonProperty("filename")]
        public String Filename { get; set; }

        public UploadFileRequest(String container, String filePath, String filename)
        {
            Container = container;
            FilePath = filePath;
            Filename = filename;
        }
    }
}