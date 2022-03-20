using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BSTeamSearch.Repositories.Realisation
{
    public class ApplicationRepository : IApplicationRepository
    {

        private readonly DBContent _db;
        public ApplicationRepository(DBContent DataBase)
        {
            _db = DataBase;
        }
        public Application GetApplication(int applicationId)
        {
            var application = _db.Application.Include(c => c.Brawler)
                                  .Include(c => c.User)
                                  .FirstOrDefault(c => c.Id == applicationId);

            if(application is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            return application;
        }
        public IEnumerable<Application> GetAllApplications()
        {
            return _db.Application.Include(c => c.Brawler)
                                  .Include(c => c.User)
                                  .ToList();
        }

        public IEnumerable<Application> GetUserApplications(string userName)
        {
            User user = _db.User.Include(c => c.Applications).FirstOrDefault(c => c.Name == userName);

            if (user is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }
            var userApplications = user.Applications;
            foreach (var application in userApplications)
            {
                application.Brawler = _db.Brawler.FirstOrDefault(c => c.Name == application.BrawlerName);

            }
            return user.Applications;
        }

        public void Delete(Application application, string userName)
        {
            User user = _db.User.Include(c => c.Applications).FirstOrDefault(c => c.Name == userName);
            if (user is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }
            if (user.Applications.IndexOf(application) == -1)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            _db.Application.Remove(application);
            user.Applications.Remove(application);
            _db.SaveChanges();
        }
    }
}
