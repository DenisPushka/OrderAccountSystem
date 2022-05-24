using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _db;

        public ClientRepository(ApplicationContext db) => _db = db;

        public async Task<List<Client>> Add(Client client)
        {
            _db.Clients.Add(client);
            await _db.SaveChangesAsync();
            return await _db.Clients.ToListAsync();
        }

        public async Task<List<Client>> Delete(Guid id)
        {
            foreach (var client in _db.Clients)
                if (client.Id == id)
                {
                    _db.Clients.Remove(client);
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Clients.ToListAsync();
        }

        public async Task<List<Client>> Insert(Client client)
        {
            foreach (var dbClient in _db.Clients)
                if (dbClient.Id == client.Id)
                {
                    dbClient.Name = client.Name;
                    dbClient.OrderId = client.OrderId;
                    break;
                }

            await _db.SaveChangesAsync();
            return await _db.Clients.ToListAsync();
        }

        public async Task<Client[]> GetArray() => await _db.Clients.ToArrayAsync();

        public async Task<List<Client>> AddOrder(Guid[] id) => await AddOrDeleteOrder(id[0], id[1], 'a');
        
        public async Task<List<Client>> DeleteOrder(Guid[] id) => await AddOrDeleteOrder(id[0], id[1], 'd');

        private async Task<List<Client>> AddOrDeleteOrder(Guid id, Guid idOrder, char method)
        {
            var order = false;
            foreach (var dbOrder in _db.Orders)
                if (dbOrder.Id == idOrder)
                    order = true;

            if (!order)
                throw new Exception("Данного заказа нет");

            foreach (var client in _db.Clients)
                if (client.Id == id && method.Equals('a'))
                {
                    client.OrderId.Add(idOrder);
                    break;
                }
                else if (client.Id == id && method.Equals('d'))
                {
                    client.OrderId.Remove(idOrder);
                    break;
                }
            
            await _db.SaveChangesAsync();
            return await _db.Clients.ToListAsync();
        }

        public Client GetClient(Guid id)
        {
            foreach (var client in _db.Clients)
                if (client.Id == id)
                    return client;

            return new Client();
        }
    }
}