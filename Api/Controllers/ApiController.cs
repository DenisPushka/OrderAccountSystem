using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController, Route("api/v1/auth")]
    public class ApiController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;

        public ApiController(IOrderRepository orderRepository) => _orderRepository = orderRepository;
        public ApiController(IClientRepository clientRepository) => _clientRepository = clientRepository;

        public ApiController(IClientRepository clientRepository, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }
        
        [HttpGet("order/{id:guid}")]
        public IActionResult GetOrder([FromRoute] Guid id)
        {
            return Ok(_orderRepository.GetOrder(id));
        }

        [HttpGet("client/{id:guid}")]
        public IActionResult GetClient([FromRoute] Guid id)
        {
            return Ok(_clientRepository.GetClient(id));
        }

        [HttpGet]
        public async Task<Order[]> Get()
        {
            return await _orderRepository.GetArrayOrder();
        }

        //[HttpGet]
        //public async Task<Client[]> GetArrayClients() => await _clientRepository.GetArrayClient();

        [HttpPost("addOrder")]
        public async Task<List<Order>> AddOrder([FromBody] Order order) => await _orderRepository.AddOrder(order);

        [HttpPost("addClient")]
        public async Task<List<Client>> AddClient([FromBody] Client client) => await _clientRepository.AddClient(client);
    }
}