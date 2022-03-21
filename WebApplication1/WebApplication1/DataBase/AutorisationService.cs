using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;

namespace BSTeamSearch.DataBase
{
    public static class AutorisationService
    {
        public static bool UserIsRegistered(IUserRepository userRepository, string userName)
        {
            try
            {
                userRepository.Get(userName);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool PasswordIsCorrect(IUserRepository userRepository, User user)
        {
            User checkUs = userRepository.Get(user.Name);

            if(checkUs is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            if(user.Password == checkUs.Password)
            {
                return true;
            }
            return false;
        }
    }
}
