using System.Linq;
using AutoMapper;
using SupportecCustomer.API.Dtos;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                    })
                .ForMember(dest => dest.Company, opt => {
                    opt.MapFrom(src => src.Company.Name);
                    });
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                    });
            CreateMap<Photo, PhotosForDetailDto>();
            CreateMap<Company, CompanyForDetailDto>();
            CreateMap<Item, ItemForProductDto>();
            CreateMap<Product, ProductForDetailDto>()
                .ForMember(dest => dest.Items, opt => {
                    opt.MapFrom(src => src.ProductItems.Select(y => y.Item).ToList());
                });
            CreateMap<Product, ProductForListDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}