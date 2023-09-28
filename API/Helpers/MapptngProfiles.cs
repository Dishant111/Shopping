using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MapptngProfiles : Profile
    {
        public MapptngProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d=>d.PictureURl,o=>o.MapFrom<ProductUrlResolver>());

        }
    }
}
