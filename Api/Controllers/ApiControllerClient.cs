using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController, Route("api/v1/client")]
    public class ApiControllerClient : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ApiControllerClient(IClientRepository clientRepository) => _clientRepository = clientRepository;

        [HttpGet("{id:guid}")]
        public IActionResult Get([FromRoute] Guid id) => Ok(_clientRepository.GetClient(id));
        
        [HttpGet]
        public async Task<Client[]> GetArray() => await _clientRepository.GetArray();

        [HttpPost("add")]
        public async Task<List<Client>> Add([FromBody] Client client) =>
            await _clientRepository.Add(client);

        [HttpPost("delete")]
        public async Task<List<Client>> Delete([FromBody] Guid id) => await _clientRepository.Delete(id);

        [HttpPost("insert")]
        public async Task<List<Client>> Insert([FromBody] Client client) =>
            await _clientRepository.Insert(client);

        [HttpPost("addOrder")]
        public async Task<List<Client>> AddOrder([FromBody] Guid[] id) =>
            await _clientRepository.AddOrder(id);
        
        [HttpPost("deleteOrder")]
        public async Task<List<Client>> DeleteOrder([FromBody]Guid[] id) =>
            await _clientRepository.DeleteOrder(id);
    }
}