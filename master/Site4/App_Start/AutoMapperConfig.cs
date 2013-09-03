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
                  .ForMember(dest => dest.Medium640Width, opt => opt.NullSubstitute(640))
                  .ForMember(dest => dest.IsLicensed, opt => opt.MapFrom(src => src.IsLicensed()));

            Mapper.CreateMap<GroupInfo, ListItem>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GroupId))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GroupName));

            Mapper.CreateMap<Contact, ListItem>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PathAlias) ? src.UserId : src.PathAlias))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                  .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.BuddyIconUrl));

        }
    }
}