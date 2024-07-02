﻿using Microsoft.EntityFrameworkCore;
using NadinSoft.Common.DTOs;
using NadinSoft.Common.Extensions;
using NadinSoft.DataBase.Contexts;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Repositories;
using System.ComponentModel;
using System.Linq.Expressions;

namespace NadinSoft.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NadinSoftContext _context;

        public ProductRepository(NadinSoftContext context)
        {
            _context = context;
        }

        public IQueryable<Product> AsQueryable(Expression<Func<Product, bool>> filter = null, params Expression<Func<Product, object>>[] includes)
        {
            var products = _context.Products.AsQueryable();

            if (filter != null)
            {
                products = products.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    products = products.Include(include);
                }
            }

            return products;
        }

        public async Task<bool> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _context.Products.Remove(product);

            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> ExistAsync(Expression<Func<Product, bool>> filter)
        {
            var isExist = await _context.Products.AnyAsync(filter);

            return isExist;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _context.Products.Update(product);

            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }
}
