using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.RoleServices;
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
    public class RoleController : ControllerBase
    {
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<Role> _roleService;
        private readonly RoleRepository _roleRepository;
        public RoleController(ElamanaTakafulContext context, IDataRepository<Role> roleService)
        {
            _context = context;
            _roleService = roleService;
            _roleRepository = new(_context, _roleService);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddRole(Role role)
        {
            _roleService.Add(role);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateRole(Role role)
        {
            _roleService.Update(role);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteRole([FromRoute] int id)
        {
            var existingRole = _roleService.Get(id);
            if (existingRole != null)
            {
                _roleService.Delete(existingRole.RoleId);
                return Ok();
            }
            return NotFound($"Role Not Found with ID : {existingRole.RoleId}");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public Role GetRole([FromRoute] int id)
        {
            return _roleService.Get(id);
        }

        [HttpGet]
        [Route("[action]/{name}")]
        public Role GetRoleByName([FromRoute] string name)
        {
            return _roleRepository.GetRoleByName(name);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult AddRoleFunctions([FromRoute] int id, [FromBody] List<Function> newFunctions)
        {
            _roleRepository.AddRoleFunctions(id, newFunctions);
            return Ok();
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult DeleteRoleFunctions([FromRoute] int id, [FromBody] List<int> oldFunctionsIds)
        {
            _roleRepository.DeleteRoleFunctions(id, oldFunctionsIds);
            return Ok();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public List<string> GetRolesByFunction([FromRoute] int id)
        {
            return _roleRepository.GetRolesByFunction(id);
        }



    }
}
