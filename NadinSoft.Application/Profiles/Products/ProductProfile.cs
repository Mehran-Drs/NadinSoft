using AutoMapper;
using NadinSoft.Application.CQRS.Products.Queries.GetProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProductsList;
using NadinSoft.Domain.Entities.Products;

namespace NadinSoft.Application.Profiles.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductsListResult>();

            CreateMap<Product, GetProductQueryResult>();
        }
    }
}
