using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.FunctionServices;
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
    public class FunctionController : Controller
    {
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<Function> _functionService;
        private readonly FunctionRepository _functionRepository;

        public FunctionController(ElamanaTakafulContext context, IDataRepository<Function> functionService)
        {
            _context = context;
            _functionService = functionService;
            _functionRepository = new(_context);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Function> GetFunctions()
        {
            return _functionService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddFunction(Function function)
        {
            _functionService.Add(function);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateFunction(Function function)
        {
            _functionService.Update(function);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteFunction([FromRoute] int id)
        {
            var existingFunction = _functionService.Get(id);
            if (existingFunction != null)
            {
                _functionService.Delete(existingFunction.FunctionId);
                return Ok();
            }
            return NotFound($"Function Not Found with ID : {existingFunction.FunctionId}");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public Function GetFunction([FromRoute] int id)
        {
            return _functionService.Get(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public List<Function> GetFunctionsByRole([FromRoute] int id)
        {
            return _functionRepository.GetFunctionsByRole(id);
        }

        [HttpGet]
        [Route("[action]")]
        public List<Function> GetActiveFunctions()
        {
            return _functionRepository.GetActiveFunctions();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public Function ActivateFunction([FromRoute] int id)
        {
            return _functionRepository.ActivateFunction(id);
        }
        
        [HttpPut]
        [Route("[action]/{id}")]
        public Function DeactivateFunction([FromRoute] int id)
        {
            return _functionRepository.DeactivateFunction(id);
        }


    }
}
