using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.UserServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElamanaTakaful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataRepository<User> _userService;
        private readonly ElamanaTakafulContext _context;
        private readonly UserRepository _userRepository;
        public UserController(ElamanaTakafulContext context, IDataRepository<User> userService)
        {
            _context = context;
            _userService = userService;
            _userRepository = new(_context, _userService);
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetAll();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddUser(User user)
        {
            _userService.Add(user);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateUser(User user)
        {
            _userService.Update(user);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            /*
            var existingUser = _userService.Get(id);
            if (existingUser != null)
            {
                _userService.Delete(existingUser.UserId);
                return Ok();
            }
            return NotFound($"User Not Found with ID : {existingUser.UserId}");
            */
            _userRepository.DeactivateUser(id);
            return Ok();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public User GetUser([FromRoute] int id)
        {
            return _userService.Get(id);
        }

        [HttpGet]
        [Route("[action]/{login}")]
        public User GetUserByLogin([FromRoute] string login)
        {
            return _userRepository.GetUserByLogin(login);
        }



        [HttpPut]
        [Route("[action]/{oldPassword}")]
        public IActionResult UpdatePassword(User user, [FromRoute] string oldPassword)
        {
            _userRepository.UpdateUserPassword(user, oldPassword);
            return Ok();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult DeactivateUser([FromRoute] int id)
        {
            _userRepository.DeactivateUser(id);
            return Ok();
        }


        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult ActivateUser([FromRoute] int id)
        {
            _userRepository.ActivateUser(id);
            return Ok();
        }

    }
}
