using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaparaHomeworkAPI.Entities;

namespace PaparaHomeworkAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "password"}
        };
        public User Authenticate(string username, string password)
        {
            return _users.SingleOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}