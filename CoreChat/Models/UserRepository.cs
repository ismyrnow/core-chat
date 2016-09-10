using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class UserRepository : IUserRepository
    {
        private int _id;
        private static List<User> _users = new List<User>();

        public UserRepository()
        {
            _id = 0;

            // Add test users.

            _users.Add(new User
            {
                ID = _id++,
                Name = "Alice",
                Email = "alice@example.com",
                Password = "foo",
                Token = "bb5bc606-77e7-4832-afc6-acf895cd5099"
            });

            _users.Add(new User
            {
                ID = _id++,
                Name = "Bob",
                Email = "bob@example.com",
                Password = "foo",
                Token = "e7e1e36c-f184-4832-944f-c52720c75f1"
            });
        }

        public User Add(User user)
        {
            user.ID = _id;
            user.Token = Guid.NewGuid().ToString();

            _users.Add(user);
            _id++;

            return user;
        }

        public User FindByID(int id)
        {
            return _users.Find(x => x.ID == id);
        }

        public User FindByToken(string token)
        {
            return _users.Find(x => x.Token == token);
        }

        public User FindByEmail(string email)
        {
            return _users.Find(x => x.Email == email);
        }

        public void Update(User user)
        {
            var storedUser = _users.Find(x => x.Email == user.Email);
            storedUser = user;
        }
    }
}
