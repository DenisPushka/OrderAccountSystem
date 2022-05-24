using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Controllers
{
    [ApiController, Route("api/v1/order")]
    public class ApiController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public ApiController(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        [HttpGet("{id:guid}")]
        public IActionResult Get([FromRoute] Guid id) => Ok(_orderRepository.GetOrder(id));
        
        [HttpGet]
        public async Task<Order[]> GetArray() => await _orderRepository.GetArray();


        [HttpPost("add")]
        public async Task<List<Order>> Add([FromBody] Order order) => await _orderRepository.Add(order);


        [HttpPost("delete")]
        public async Task<List<Order>> Delete([FromBody] Guid id) => await _orderRepository.Delete(id);

        [HttpPost("insert")]
        public async Task<List<Order>> Insert([FromBody] Order order) =>
            await _orderRepository.Insert(order);

        [HttpPost("addProduct")]
        public async Task<List<Order>> AddProductInOrder([FromBody] object[] ar) =>
            await _orderRepository.AddProductInOrder(ar);

        [HttpPost("deleteProduct")]
        public async Task<List<Order>> DeleteProduct([FromBody] object[] ar) =>
            await _orderRepository.DeleteProduct(ar);
    }
}