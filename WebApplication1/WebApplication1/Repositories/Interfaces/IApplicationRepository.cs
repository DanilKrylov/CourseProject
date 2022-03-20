using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        public IEnumerable<Application> GetAllApplications();

        public IEnumerable<Application> GetUserApplications(string UserName);

        public void Delete(Application application, string userName);

        public Application GetApplication(int applicationId);
    }
}
