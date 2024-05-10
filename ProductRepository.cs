using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheFunctionAppAzure.Db;
using TheFunctionAppAzure.Interfaces;
using TheFunctionAppAzure.Models;

namespace TheFunctionAppAzure
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _dbContext;

        public ProductRepository(MyDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<Product> GetByIdAsync(string id)
        {
            return await _dbContext.products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            _dbContext.products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.products.Update(product);
             await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var productToDelete = await _dbContext.products.FindAsync(id);
            if(productToDelete !=null)
            {
                _dbContext.products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
