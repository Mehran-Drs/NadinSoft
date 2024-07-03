using AutoMapper;
using NadinSoft.Application.CQRS.Products.Commands.CreateProduct;
using NadinSoft.Application.CQRS.Products.Commands.EditProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProductsList;
using NadinSoft.Domain.Entities.Products;

namespace NadinSoft.Application.Profiles.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductsListResult>()
                .ForMember(dest => dest.CreatorFullName, opt => opt.MapFrom(src => src.Creator.ToString()));

            CreateMap<Product, GetProductQueryResult>();

            CreateMap<CreateProductCommand , Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<EditProductCommand, Product>()
            .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
