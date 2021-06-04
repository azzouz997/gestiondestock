using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.OrderServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ElamanaTakaful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<Order> _orderService;
        private readonly OrderRepository _orderRepository;

        public OrderController(ElamanaTakafulContext context, IDataRepository<Order> orderService)
        {
            _context = context;
            _orderService = orderService;
            _orderRepository = new(_context);
        }


        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Order> GetOrders()
        {
            return _orderService.GetAll();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("[action]")]
        public IActionResult AddOrder()
        {
            string fileId = "";
            Order order = new();
            try
            {
                order = JsonConvert.DeserializeObject<Order>(Request.Form["data"]);
                Debug.Print("--> " + order.Quantity);
                var file = Request.Form.Files[0];
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var id = _orderRepository.UploadFromStream(fileName, file.OpenReadStream());
                fileId = id.ToString();
            }
            catch (Exception ex)
            {
                StatusCode(500, $"Internal server error: {ex}");
            }
            order.OrderReceiptId = fileId;

            _orderService.Add(order);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateOrder(Order order)
        {
            _orderService.Update(order);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteOrder([FromRoute] int id)
        {
            var existingOrder = _orderService.Get(id);
            if (existingOrder != null)
            {
                _orderService.Delete(existingOrder.OrderId);
                return Ok();
            }
            return NotFound($"Order Not Found with ID : {existingOrder.OrderId}");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public Order GetOrder([FromRoute] int id)
        {
            return _orderService.Get(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public List<Order> GetOrdersByCreator([FromRoute] int id)
        {
            return _orderRepository.GetOrdersByCreator(id);
        }

        [HttpGet]
        [Route("[action]/{fileName}")]
        public GridFSDownloadStream GetOrderReceipt([FromRoute] string fileName)
        {
            var stream = _orderRepository.DownloadFromStream(fileName);
            var name = stream?.FileInfo.Filename;
            Response.ContentType = name;
            return stream;
        }




    }
}
