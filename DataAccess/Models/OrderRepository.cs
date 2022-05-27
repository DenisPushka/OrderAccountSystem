using System;
using System.Collections.Generic;
using System.Linq;
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
            await DeleteOrderInClient(id);
            foreach (var order in _db.Orders)
                if (order.Id == id)
                {
                    _db.Orders.Remove(order);
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Orders.ToListAsync();
        }

        private async Task DeleteOrderInClient(Guid id)
        {
            foreach (var client in _db.Clients)
            {
                var buff = client.OrderId.FirstOrDefault(p => p == id);
                if (buff.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                    continue;
                client.OrderId.Remove(buff);
            }

            await _db.SaveChangesAsync();
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

        public async Task<List<Order>> AddProductInOrder(object[] ar)
        {
            var g = new Guid(ar[0].ToString() ?? string.Empty);
            var g2 = new Guid(ar[1].ToString() ?? string.Empty);
            var number = int.Parse(ar[2].ToString() ?? string.Empty);
            return await AddOrDeleteProduct(g, g2, number, 'a');
        }

        public async Task<List<Order>> DeleteProduct(object[] ar)
        {
            var g = new Guid(ar[0].ToString() ?? string.Empty);
            var g2 = new Guid(ar[1].ToString() ?? string.Empty);
            var number = int.Parse(ar[2].ToString() ?? string.Empty);
            return await AddOrDeleteProduct(g, g2, number, 'd');
        }

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