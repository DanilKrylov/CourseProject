using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;

namespace BSTeamSearch.Services
{
    public static class AutorisationService
    {
        public static bool UserIsRegistered(DBContent db, User user)
        {
            if(db.User.FirstOrDefault(c => c.Name == user.Name) is null)
            {
                return false;
            }

            return true;
        }

        public static bool PasswordIsCorrect(DBContent db, User user)
        {
            User checkUs = db.User.FirstOrDefault(c => c.Name == user.Name);

            if(checkUs is null)
            {
                #warning сделать обработку ошибки
                throw new ObjectNotFoundInDataBaseException();
            }

            return checkUs.CheckPassword(user.Password);
        }
    }
}
