using AutoMapper;
using Azure.Storage.Blobs;

namespace Munizoft.Azure.Services.Mappings
{
    public class AzureMapping : Profile
    {
        public AzureMapping()
        {
            #region Blob
            this.CreateMap<BlobClient, Models.BlobInfo>()
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.URL, opt => opt.MapFrom(src => src.Uri.AbsoluteUri))
                .AfterMap<GetBlobPropertiesAction>();
                ;
            #endregion Blob
        }
    }
}