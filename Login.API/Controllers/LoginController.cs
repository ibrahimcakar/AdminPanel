using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private  IUserOperation _users;
        public LoginController(IUserOperation repository)
        {
            _users = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _users.GetAllUser();
            return Ok(list);
        }
    }
}
