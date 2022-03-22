using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;

namespace BSTeamSearch.Repositories.Realisation
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContent _db;
        public UserRepository(DBContent DataBase)
        {
            _db = DataBase;
        }

        public IEnumerable GetAll()
        {
            var users = _db.User.ToList();
            return users;
        }

        public User Get(string userName)
        {
            var user = _db.User.FirstOrDefault(c => c.Name == userName);

            if(user is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            return user;
        }

        public void Add(User user)
        {
            _db.User.Add(user);
            _db.SaveChanges();
        }
    }
}
