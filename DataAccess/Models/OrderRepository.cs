using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _db;

        public OrderRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<List<Order>> AddOrder(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return await _db.Orders.ToListAsync();
        }

        public async Task<List<Order>> DeleteOrder(Guid id)
        {
            foreach (var order in _db.Orders)
                if (order.Id == id)
                {
                    _db.Orders.Remove(order);
                    await _db.SaveChangesAsync();
                    break;
                }

            return await _db.Orders.ToListAsync();
        }

        public async Task<List<Order>> InsertOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<Order[]> GetArrayOrder() => await _db.Orders.ToArrayAsync();

        public Order GetOrder(Guid id)
        {
            foreach (var order in _db.Orders)
                if (order.Id == id)
                    return order;

            return new Order();
        }
    }
}