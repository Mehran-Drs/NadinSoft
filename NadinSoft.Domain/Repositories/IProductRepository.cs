using NadinSoft.Domain.Entities.Products;
using System.Linq.Expressions;

namespace NadinSoft.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<bool> CreateAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(Product product);

        IQueryable<Product> AsQueryable(Expression<Func<Product, bool>> filter = null, params Expression<Func<Product, object>>[] includes);

        Task<Product> GetByIdAsync(int id);

        Task<bool> AnyAsync(Expression<Func<Product, bool>> filter);  
    }
}
