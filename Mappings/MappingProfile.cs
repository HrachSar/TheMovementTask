using AutoMapper;
using MyProject.DTOs;
using MyProject.Models;

namespace MyProject.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
            
            CreateMap<Product, ProductReadDTO>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src =>
                        src.ProductCategories.Select(pc => pc.Category).ToList()));

            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());

            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());
        }
    }
}

