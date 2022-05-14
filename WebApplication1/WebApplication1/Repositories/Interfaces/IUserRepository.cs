using System.Collections;
using BSTeamSearch.Models;
using BSTeamSearch.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable GetAll(string seacrhString);

        public User Get(string userName);

        public void Add(RegistrationViewModel user);

        public bool UserIsRegistered(string userName);

        public bool PasswordIsCorrect(string userName, string password);

        public void ChecklUniquenessOfModel(RegistrationViewModel registrationViewModel, ModelStateDictionary modelState);

        public void BanUser(string userName);

        public void UnbanUser(string userName);
    }
}
