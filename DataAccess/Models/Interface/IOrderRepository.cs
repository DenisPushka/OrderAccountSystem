using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Models.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> Add(Order order);
        Task<List<Order>> Delete(Guid id);
        Task<List<Order>> Insert(Order order);
        Task<Order[]> GetArray();
        Task<List<Order>> AddProductInOrder(object[] ar);
        Task<List<Order>> DeleteProduct(object[] ar);
        Order GetOrder(Guid id);
    }
}