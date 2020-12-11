using AutoMapper;
using Azure.Storage.Blobs;

namespace Munizoft.Azure.Services.Mappings
{
    public class GetBlobPropertiesAction : IMappingAction<BlobClient, Models.BlobInfo>
    {
        public void Process(BlobClient source, Models.BlobInfo destination, ResolutionContext context)
        {
            var properties = source.GetProperties();

            destination.CreatedOn = properties.Value.CreatedOn.DateTime;
            destination.LastModifiedOn = properties.Value.LastModified.DateTime;

            destination.ContentType = properties.Value.ContentType;
            destination.Size = properties.Value.ContentLength;
        }
    }
}
