using AutoMapper;
using Clean_Architecture_Sample.Application.DTOs;
using Clean_Architecture_Sample.Domain;

namespace Clean_Architecture_Sample.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain → Response
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.ImageFile,
                opt => opt.MapFrom(src => src.ImageFile));

        // Request → Domain
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.ImageFile.FileName)); // handled manually

    }
}
