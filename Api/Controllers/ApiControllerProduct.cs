using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController, Route("api/v1/product")]
    public class ApiControllerProduct : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ApiControllerProduct(IProductRepository productRepository) => _productRepository = productRepository;
        
        [HttpGet("{id:guid}")]
        public IActionResult Get([FromRoute] Guid id) => Ok(_productRepository.Get(id));
        
        [HttpGet]
        public async Task<Product[]> GetArray() => await _productRepository.GetArray();

        [HttpPost("add")]
        public async Task<List<Product>> Add([FromBody] Product product) => await _productRepository.Add(product);
        
        [HttpPost("delete")]
        public async Task<List<Product>> Delete([FromBody] Guid id) => await _productRepository.Delete(id);

        [HttpPost("insert")]
        public async Task<List<Product>> Insert([FromBody] Product product) =>
            await _productRepository.Insert(product);
    }
}