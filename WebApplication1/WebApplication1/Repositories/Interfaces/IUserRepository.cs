using System.Collections;
using BSTeamSearch.Models;
using BSTeamSearch.ViewModels;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable GetAll(string seacrhString);

        public User Get(string userName);

        public void Add(RegistrationViewModel user);

        public bool UserIsRegistered(string userName);

        public bool PasswordIsCorrect(string userName, string password);
    }
}
