using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Models.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> Add(Product product);
        Task<List<Product>> Delete(Guid id);
        Task<List<Product>> Insert(Product product);
        Task<Product[]> GetArray();
        Product Get(Guid id);
    }
}