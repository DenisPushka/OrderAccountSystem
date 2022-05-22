using System;
using System.Collections.Generic;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Domain.Models;

namespace OrderAccountSystem
{
    public class RootController : Controller
    {
        [HttpGet("/")]
        public IActionResult Result()
        {
            using var db = new ApplicationContext();
            AddTable(db);
            return View("Index");
        }

        private static void AddTable(ApplicationContext db)
        {
            if (!db.Orders.Any() && !db.Products.Any() && !db.Clients.Any())
            {
                var arProduct = db.CreateTableProducts();
                var arClient = db.CreateTableClients();

                foreach (var product in arProduct)
                    db.Products.Add(product);
                db.SaveChanges();

                var arOrder = new[]
                {
                    new Order
                    {
                        Price = arProduct[0].Count * arProduct[0].Price, Product = arProduct[0],
                        ProductId = new List<Guid> {arProduct[0].Id}
                    },
                    new Order
                    {
                        Price = arProduct[1].Count * arProduct[1].Price, Product = arProduct[1],
                        ProductId = new List<Guid> {arProduct[1].Id}
                    },
                    new Order
                    {
                        Price = arProduct[2].Count * arProduct[2].Price, Product = arProduct[2],
                        ProductId = new List<Guid> {arProduct[2].Id}
                    }
                };
                
                foreach (var order in arOrder)
                    db.Orders.Add(order);

                db.SaveChanges();
                for (var i = 0; i < arClient.Length; i++)
                {
                    arClient[i].OrderId = arOrder[i].Id;
                    db.Clients.Add(arClient[i]);
                }


                db.SaveChanges();
            }
        }
    }
}