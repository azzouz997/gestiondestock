using ElamanaTakaful.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElamanaTakaful.Application.Common;
using Microsoft.AspNetCore.Authorization;
using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.UserServices;
using ElamanaTakaful.Infrastructure.Contexts;

namespace ElamanaTakaful.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<User> _userService;
        private readonly UserRepository _userRepository;

        public AuthenticationController(ElamanaTakafulContext context, IJwtAuthenticationManager jwtAuthenticationManager, IDataRepository<User> userService)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _userService = userService;
            _userRepository = new(_context, _userService);
        }

        [HttpGet]
        public string Get()
        {
            return ("Hello");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            jwtAuthenticationManager.Users = _userService.GetAll();

            var token = jwtAuthenticationManager.Authenticate(userCred.Login, userCred.Password);
            if (token == null)
                return Unauthorized();
            return Ok(new
                {
                    token = token,
                    login = userCred.Login,
                    role = _userRepository.GetUserByCred(userCred).Role.Name
            });
        }
    }
}
