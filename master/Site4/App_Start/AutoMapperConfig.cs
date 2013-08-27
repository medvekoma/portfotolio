using AutoMapper;
using FlickrNet;
using Portfotolio.Domain;
using Portfotolio.FlickrEngine;

namespace Portfotolio.Site4
{
    public class AutoMapperConfig
    {
        public static void ConfigureAutoMappings()
        {
            Mapper.CreateMap<Photo, DomainPhoto>()
                  .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                  .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.OwnerName))
                  .ForMember(dest => dest.AuthorAlias, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PathAlias) ? src.UserId : src.PathAlias))
                  .ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.WebUrl + "lightbox/"))
                  .ForMember(dest => dest.SmallWidth, opt => opt.NullSubstitute(240))
                  .ForMember(dest => dest.IsLicensed, opt => opt.MapFrom(src => src.IsLicensed()));
        }
    }
}