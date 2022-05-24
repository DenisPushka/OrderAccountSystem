using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Models.Interface
{
    public interface IClientRepository
    {
        Task<List<Client>> Add(Client client);
        Task<List<Client>> Delete(Guid id);
        Task<List<Client>> Insert(Client client);
        Task<Client[]> GetArray();
        Client GetClient(Guid id);
        Task<List<Client>> AddOrder(Guid[] id);
        Task<List<Client>> DeleteOrder(Guid[] id);
    }
}