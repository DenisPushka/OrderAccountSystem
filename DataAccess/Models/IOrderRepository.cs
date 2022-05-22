using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Models
{
    public interface IOrderRepository
    {
        Task<List<Order>> AddOrder(Order order);
        Task<List<Order>> DeleteOrder(Guid id);
        Task<List<Order>> InsertOrder(Order order);
        Task<Order[]> GetArrayOrder();
        Order GetOrder(Guid id);
    }
}