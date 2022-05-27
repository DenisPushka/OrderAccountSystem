using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _db;

        public ProductRepository(ApplicationContext db) => _db = db;

        public async Task<List<Product>> Add(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return await _db.Products.ToListAsync();
        }

        public async Task<List<Product>> Delete(Guid id)
        {
            await DeleteProductInOrder(id);
            foreach (var product in _db.Products)
                if (product.Id == id)
                {
                    _db.Products.Remove(product);
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Products.ToListAsync();
        }

        private async Task DeleteProductInOrder(Guid id)
        {
            foreach (var order in _db.Orders)
            {
                var buff = order.ProductId.FirstOrDefault(p => p == id);
                if (buff.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                    continue;
                order.ProductId.Remove(buff);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<List<Product>> Insert(Product product)
        {
            foreach (var dbProduct in _db.Products)
                if (dbProduct.Id == product.Id)
                {
                    dbProduct.Price = product.Price;
                    dbProduct.Count = product.Count;
                    dbProduct.Description = product.Description;
                    dbProduct.Name = product.Name;
                    dbProduct.IsStock = product.IsStock;
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Products.ToListAsync();
        }

        public async Task<Product[]> GetArray() => await _db.Products.ToArrayAsync();

        public Product Get(Guid id)
        {
            foreach (var product in _db.Products)
                if (product.Id == id)
                    return product;

            return new Product();
        }
    }
}