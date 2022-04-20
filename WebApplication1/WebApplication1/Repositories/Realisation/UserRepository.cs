using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using BSTeamSearch.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            var users = _db.User.Include(c => c.Discord).ToList();
            return users;
        }

        public User Get(string userName)
        {
            var user = _db.User.Include(c => c.Discord).FirstOrDefault(c => c.Name == userName);

            if(user is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            return user;
        }

        public void Add(RegistrationViewModel user)
        {
            _db.User.Add(new User(user.Name, user.Password));
            _db.Discords.Add(new Discord(user.DiscordName, user.DiscordSharp, user.Name));
            _db.SaveChanges();
        }
    }
}
