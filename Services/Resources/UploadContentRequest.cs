using System;

namespace Munizoft.Azure.Services.Resources
{
    public class UploadContentRequest
    {
        public String Container { get; private set; }
        public String Content { get; private set; }
        public String Filename { get; private set; }

        public UploadContentRequest(String container, String content, String filename)
        {
            Container = container;
            Content = content;
            Filename = filename;
        }
    }
}