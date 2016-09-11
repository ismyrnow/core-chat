using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreChat.Models;
using CoreChat.Filters;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreChat.Controllers
{
    [Route("[controller]")]
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

            return Json(BuildUserResponse(user));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] RegisterUser login)
        {
            var user = Users.FindByEmail(login.Email);

            if (user == null || login.Password != user.Password)
            {
                return new UnauthorizedResult();
            }

            return Json(BuildUserResponse(user));
        }

        [HttpGet("Me")]
        [BearerToken]
        public IActionResult ViewMe()
        {
            string token = User.FindFirst("token").Value;
            User user = Users.FindByToken(token);

            if (user == null)
            {
                return Unauthorized();
            }

            return Json(BuildUserResponse(user));
        }

        [HttpPut("Me")]
        [BearerToken]
        public IActionResult EditMe([FromBody] RegisterUser edit)
        {
            string token = User.FindFirst("token").Value;
            User user = Users.FindByToken(token);

            if (user == null)
            {
                return Unauthorized();
            }

            user.Name = edit.Name;
            user.Email = edit.Email;

            Users.Update(user);

            return Json(BuildUserResponse(user));
        }

        private object BuildUserResponse(User user)
        {
            return new
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
        }
    }
}
