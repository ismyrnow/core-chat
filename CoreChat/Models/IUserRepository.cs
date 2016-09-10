using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public interface IUserRepository
    {
        User Add(User user);
        User Find(int id);
        void Update(User user);
    }
}
