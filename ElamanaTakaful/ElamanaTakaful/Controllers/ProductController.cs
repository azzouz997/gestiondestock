using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.ProductServices;
using ElamanaTakaful.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElamanaTakaful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IDataRepository<Product> _productService;
        private readonly ProductRepository _productRepository;
        public ProductController(IDataRepository<Product> productService, ProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProducts()
        {
            IEnumerable<Product> products = _productService.GetAll();
            return Ok(products);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddProduct(Product product)
        {
            _productService.Add(product);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateProduct(Product product)
        {
            _productService.Update(product);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productService.Get(id);
            if (existingProduct != null)
            {
                _productService.Delete(existingProduct.ProductId);
                return Ok();
            }
            return NotFound($"Product Not Found with ID : {existingProduct.ProductId}");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public Product GetProduct([FromRoute] int id)
        {
            return _productService.Get(id);
        }

        [HttpGet]
        [Route("[action]/{SearchProductName}")]
        public List<Product> SearchProductByName([FromRoute] string SearchProductName)
        {
            return _productRepository.FindProductByName(SearchProductName);
        }

        [HttpGet]
        [Route("[action]/{SearchProductCode}")]
        public List<Product> SearchProductByCode([FromRoute] string SearchProductCode)
        {
            return _productRepository.FindProductByName(SearchProductCode);
        }

        [HttpGet]
        [Route("[action]/{SearchProductSupplier}")]
        public List<Product> SearchProductBySupplier([FromRoute] string SearchProductSupplier)
        {
            return _productRepository.FindProductBySupplier(SearchProductSupplier);
        }
    }
}
