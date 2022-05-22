using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _db;

        public ClientRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<List<Client>> AddClient(Client client)
        {
            _db.Clients.Add(client);
            await _db.SaveChangesAsync();
            return await _db.Clients.ToListAsync();
        }

        public async Task<List<Client>> DeleteClient(Guid id)
        {
            foreach (var client in _db.Clients)
                if (client.Id == id)
                {
                    _db.Clients.Remove(client);
                    await _db.SaveChangesAsync();
                    break;
                }

            return await _db.Clients.ToListAsync();
        }

        public async Task<List<Client>> InsertClient(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task<Client[]> GetArrayClient() => await _db.Clients.ToArrayAsync();

        public Client GetClient(Guid id)
        {
            foreach (var client in _db.Clients)
                if (client.Id == id)
                    return client;

            return new Client();
        }
    }
}