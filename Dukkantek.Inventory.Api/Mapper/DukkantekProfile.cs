using AutoMapper;
using Dukkantek.Inventory.Application.Categories.Commands;
using Dukkantek.Inventory.Application.Categories.Models.Result;
using Dukkantek.Inventory.Application.Products.Commands;
using Dukkantek.Inventory.Application.Products.Models.Results;
using Dukkantek.Inventory.Domain.Categories;
using Dukkantek.Inventory.Domain.Products;

namespace Dukkantek.Inventory.Api.Mapper
{
    public class DukkantekProfile : Profile
    {
        public DukkantekProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<Category, GetCategoryQueryResult>()
                .ForMember(e => e.Products, opt => opt.MapFrom(s => s.Products.Count));

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, GetProductQueryResult>()
                .ForMember(e => e.CategoryName, opt => opt.MapFrom(s => s.Category.Name));



        }
    }
}
