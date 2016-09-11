using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreChat.Models;
using CoreChat.Filters;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreChat.Controllers
{
    [Route("[controller]")]
    public class ChatsController : Controller
    {
        private IChatRepository Chats;
        private IUserRepository Users;

        public ChatsController(IChatRepository chats, IUserRepository users)
        {
            Chats = chats;
            Users = users;
        }

        [HttpGet]
        [BearerToken]
        public IActionResult Index(string q, int page, int limit)
        {
            string token = User.FindFirst("token").Value;
            User user = Users.FindByToken(token);

            if (user == null)
            {
                return Unauthorized();
            }

            var chats = Chats.GetChats(q, page, limit);

            return Json(new
            {
                success = true,
                data = chats.Data,
                pagination = chats.Pagination
            });
        }

        [HttpGet("{chatId}/messages")]
        [BearerToken]
        public IActionResult Messages(int chatId, int page, int limit)
        {
            string token = User.FindFirst("token").Value;
            User user = Users.FindByToken(token);

            if (user == null)
            {
                return Unauthorized();
            }

            var messages = Chats.GetMessages(chatId, page, limit);

            return Json(new
            {
                success = true,
                data = messages.Data,
                pagination = messages.Pagination
            });
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
