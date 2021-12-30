using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private  IUserOperation _users;
        public LoginController(IUserOperation repository)
        {
            _users = repository;
        }

        [HttpGet]
        [Route("[action]/(id)")]
        public IActionResult GetUserById(int id)
        {
            var user = _users.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult GetLogin(User model)
        {
            var list = _users.GetUserByMailPassword(model);
            if (list != null)
            {
                return Ok(new
                {
                    Id = list.Id,
                    FullName = list.FullName,
                    Username = list.UserName,
                    Email = list.Email,
                    IsActive = list.IsActive,
                    IsAdmin = list.IsAdmin
                });
            }
            else
            {
                ViewBag.Error = "Kullanıcı adı veya şifre yanlış";
                return Forbid();
            }

        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _users.GetAllUser();
            return Ok(list);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateUser([FromBody] User user)
        {
            var createdUser = _users.CreateUser(user);
            return CreatedAtAction("Get", new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (_users.GetUserById(user.Id) != null)
            {
                return Ok(_users.UpdateUser(user));
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Route("[action]/{id}")]

        public IActionResult DeleteUser(int id)
        {
            if (_users.GetUserById(id) != null)
            {
                _users.DeleteUser(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
