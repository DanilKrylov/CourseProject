using System.Collections.Generic;
using BSTeamSearch.Models;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        public IEnumerable<Application> GetAllWithout(string userNameIgnore);

        public IEnumerable<Application> FiltrationGet(string userName, bool onlyLiked, bool cupsAscending, string searchString, int minCups, int maxCups);

        public IEnumerable<Application> GetUserApplications(string userName);

        public void Delete(Application application, string userName);

        public Application Get(int applicationId);

        public void Add(Application application);

        public void Edit(Application newApplication);
    }
}
