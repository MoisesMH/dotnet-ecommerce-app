using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using AutoMapper;
using Core.Entities;

namespace api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(
                dest => dest.ProductBrand,
                opt => opt.MapFrom(product => product.ProductBrand.Name)
            )
            .ForMember(
                dest => dest.ProductType,
                opt => opt.MapFrom(product => product.ProductType.Name)
            )
            .ForMember(
                dest => dest.PictureUrl,
                opt => opt.MapFrom<ProductUrlResolver>()
            );
    }
}