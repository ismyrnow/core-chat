using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreChat.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreChat.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public IUserRepository Users { get; set; }

        public UsersController(IUserRepository users)
        {
            Users = users;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterUser register)
        {
            if (register == null || register.Password != register.Confirm)
            {
                return BadRequest();
            }

            var user = new User
            {
                Name = register.Name,
                Email = register.Email,
                Password = register.Password
            };

            // The repository adds an ID and Token, so we need a response from it.
            user = Users.Add(user);

            var response = new
            {
                success = true,
                data = new
                {
                    id = user.ID,
                    token = user.Token,
                    email = user.Email,
                    name = user.Name
                }
            };

            return Json(response);
        }
    }
}
