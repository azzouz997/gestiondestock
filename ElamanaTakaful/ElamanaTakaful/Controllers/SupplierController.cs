using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.SupplierServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElamanaTakaful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly IDataRepository<Supplier> _supplierService;
        private readonly ElamanaTakafulContext _context;
        private readonly SupplierRepository _supplierRepository;

        public SupplierController(ElamanaTakafulContext context, IDataRepository<Supplier> supplierService)
        {
            _context = context;
            _supplierService = supplierService;
            _supplierRepository = new SupplierRepository(_context);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddSupplier(Supplier supplier)
        {
            _supplierService.Add(supplier);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateSupplier(Supplier supplier)
        {
            _supplierService.Update(supplier);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteSupplier([FromRoute] int id)
        {
            var existingSupplier = _supplierService.Get(id);
            if (existingSupplier != null)
            {
                _supplierService.Delete(existingSupplier.SupplierId);
                return Ok();
            }
            return NotFound($"Supplier Not Found with ID : {existingSupplier.SupplierId}");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public Supplier GetSupplier([FromRoute] int id)
        {
            return _supplierService.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        public List<Supplier> GetSuppliersByName([FromRoute] string searchSupplierName)
        {
            return _supplierRepository.FindSupplierByName(searchSupplierName);
        }
        
        [HttpGet]
        [Route("[action]")]
        public List<Supplier> GetSuppliersByCode([FromRoute] string searchSupplierCode)
        {
            return _supplierRepository.FindSupplierByName(searchSupplierCode);
        }
    }
}
