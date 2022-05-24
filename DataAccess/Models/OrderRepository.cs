using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _db;

        public OrderRepository(ApplicationContext db) => _db = db;

        public async Task<List<Order>> Add(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return await _db.Orders.ToListAsync();
        }

        public async Task<List<Order>> Delete(Guid id)
        {
            foreach (var order in _db.Orders)
                if (order.Id == id)
                {
                    _db.Orders.Remove(order);
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Orders.ToListAsync();
        }

        public async Task<List<Order>> Insert(Order order)
        {
            foreach (var dbOrder in _db.Orders)
                if (dbOrder.Id == order.Id)
                {
                    dbOrder.Price = order.Price;
                    dbOrder.Product = order.Product;
                    dbOrder.ProductId = order.ProductId;
                    dbOrder.QuantityGoods = order.QuantityGoods;
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Orders.ToListAsync();
        }

        public async Task<List<Order>> AddProductInOrder(object[] ar) =>
            await AddOrDeleteProduct(new Guid((byte[]) ar[0]),
                new Guid((byte[]) ar[1]),
                (int) ar[2],
                'a');

        public async Task<List<Order>> DeleteProduct(object[] ar) =>
            await AddOrDeleteProduct(new Guid(ar[0].ToString() ?? string.Empty),
                new Guid(ar[1].ToString() ?? string.Empty),
                (int) ar[2],
                'd');

        private async Task<List<Order>> AddOrDeleteProduct(Guid id, Guid idProduct, int count, char method)
        {
            Product product = null;

            foreach (var dbProduct in _db.Products)
                if (dbProduct.Id == idProduct)
                    product = dbProduct;

            if (product == null)
                throw new Exception("Данного продукта нет");

            if (product.Count < count)
                throw new Exception("\nТовара меньше, введите другое количество\n");
            if (count < 0)
                throw new Exception("\nВведено число меньше 0, введите другое количество\n");

            foreach (var dbOrder in _db.Orders)
                if (dbOrder.Id == id && method.Equals('a'))
                {
                    dbOrder.ProductId.Add(idProduct);
                    dbOrder.Price += product.Count * product.Price;
                    dbOrder.QuantityGoods += product.Count;
                    break;
                }
                else if (dbOrder.Id == id && method.Equals('d'))
                {
                    dbOrder.ProductId.Remove(idProduct);
                    dbOrder.Price -= product.Count * product.Price;
                    dbOrder.QuantityGoods -= product.Count;
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order[]> GetArray() => await _db.Orders.ToArrayAsync();

        public Order GetOrder(Guid id)
        {
            foreach (var order in _db.Orders)
                if (order.Id == id)
                    return order;

            return new Order();
        }
    }
}