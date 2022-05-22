using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Models
{
    public interface IClientRepository
    {
        Task<List<Client>> AddClient(Client client);
        Task<List<Client>> DeleteClient(Guid id);
        Task<List<Client>> InsertClient(Client client);
        Task<Client[]> GetArrayClient();
        Client GetClient(Guid id);
    }
}