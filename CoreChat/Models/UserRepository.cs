﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class UserRepository : IUserRepository
    {
        private int _id;
        private static ConcurrentDictionary<int, User> _users = new ConcurrentDictionary<int, User>();

        public UserRepository()
        {
            _id = 0;
        }

        public void Add(User user)
        {
            user.ID = _id;
            _users[user.ID] = user;
            _id++;
        }

        public User Find(int id)
        {
            User user;
            _users.TryGetValue(id, out user);
            return user;
        }

        public void Update(User user)
        {
            _users[user.ID] = user;
        }
    }
}