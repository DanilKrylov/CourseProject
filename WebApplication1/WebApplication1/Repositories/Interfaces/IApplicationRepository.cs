using System.Collections.Generic;
using BSTeamSearch.Models;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        public IEnumerable<Application> GetAll();

        public IEnumerable<Application> GetUserApplications(string UserName);

        public void Delete(Application application, string userName);

        public Application Get(int applicationId);

        public void Add(Application application, string userName);
    }
}
