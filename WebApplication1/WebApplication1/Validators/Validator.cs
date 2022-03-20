using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace BSTeamSearch.Validators
{
    public static class Validator
    {
        
        public static bool CheckSpacesInString(string str)
        {
            foreach(char elem in str)
            {
                if(elem.ToString() == " ")
                {
                    return true;
                }
            }

            return false;

        }
        public static bool UserIsRegistered(User user, DBContent dataBase)
        { 
                IEnumerable<User> users = dataBase.User;

                User currentUser = users.FirstOrDefault(c => c.Name == user.Name);

                if(currentUser is null)
                {
                    return false;
                }
                return true;
        }

    }
}
