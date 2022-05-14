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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BSTeamSearch.Repositories.Realisation
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContent _db;
        public UserRepository(DBContent dataBase)
        {
            _db = dataBase;
        }

        public IEnumerable GetAll(string seacrhString)
        {
            if (seacrhString is null)
            {
                seacrhString = string.Empty;
            }

            var users = _db.User.Where(c => c.Name.ToLower().Contains(seacrhString.ToLower()) && c.IsAdmin == false).ToList();
            return users;
        }

        public User Get(string userName)
        {
            var user = _db.User
                .Include(c => c.Applications)
                    .ThenInclude(c => c.Brawler)
                .Include(c => c.Applications)
                    .ThenInclude(c => c.Likes)
                .FirstOrDefault(c => c.Name == userName);

            if (user is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            return user;
        }

        public void Add(RegistrationViewModel user)
        {
            _db.User.Add(new User(user.Name, user.Password, user.Age, user.Email, user.CountOfCups, user.BrawlAccountName));
            _db.SaveChanges();
        }

        public bool UserIsRegistered(string userName)
        {
            var user = _db.User.FirstOrDefault(c => c.Name == userName);
            if (user is null)
            {
                return false;
            }

            return true;
        }

        public bool PasswordIsCorrect(string userName, string password)
        {
            var user = _db.User.FirstOrDefault(c => c.Name == userName);
            if (user is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            if (user.Password == password)
            {
                return true;
            }

            return false;
        }

        public void ChecklUniquenessOfModel(RegistrationViewModel registrationViewModel, ModelStateDictionary modelState)
        {
            if (registrationViewModel.Email is null || registrationViewModel.BrawlAccountName is null || registrationViewModel.Name is null)
            {
                return;
            }

            if (_db.User.FirstOrDefault(c => c.Email == registrationViewModel.Email) is not null)
            {
                modelState.AddModelError("Email", "Данный email уже занят");
            }

            if (_db.User.FirstOrDefault(c => c.BrawlAccountName == registrationViewModel.BrawlAccountName) is not null)
            {
                modelState.AddModelError("BrawlAccountName", "Данный никнейм уже занят");
            }

            if (UserIsRegistered(registrationViewModel.Name))
            {
                modelState.AddModelError("Name", "Данный логин уже занят, попробуйте какойто другой");
            }
        }

        public void BanUser(string userName)
        {
            var user = Get(userName);
            user.IsBanned = true;
            _db.User.Update(user);
            _db.SaveChanges();
        }

        public void UnbanUser(string userName)
        {
            var user = Get(userName);
            user.IsBanned = false;
            _db.User.Update(user);
            _db.SaveChanges();
        }
    }
}
