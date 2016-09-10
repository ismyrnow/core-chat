using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public interface IUserRepository
    {
        User Add(User user);
        User FindByToken(string token);
        User FindByEmail(string email);
        void Update(User user);
    }
}
