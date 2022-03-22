using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserRepository _userRepository;
        private readonly IBrawlerRepository _brawlerRepository;
        public ApplicationRepository(DBContent dataBase, IUserRepository userRepository, IBrawlerRepository brawlerRepository)
        {
            _db = dataBase;
            _userRepository = userRepository;
            _brawlerRepository = brawlerRepository;
        }
        public Application Get(int applicationId)
        {
            var application = _db.Application.Include(c => c.Brawler)
                                  .Include(c => c.User)
                                  .FirstOrDefault(c => c.Id == applicationId);

            if(application is null)
            {
                _db.Dispose();
                throw new ObjectNotFoundInDataBaseException();
            }
            return application;
        }
        public IEnumerable<Application> GetAll()
        {
            var applications = _db.Application.Include(c => c.Brawler)
                                              .Include(c => c.User)
                                              .ToList();
            return applications;
        }

        public IEnumerable<Application> GetUserApplications(string userName)
        {
            var applications = _db.Application.Where(c => c.User.Name == userName)
                                              .Include(c => c.Brawler)
                                              .Include(c => c.User)
                                              .ToList();
            if (applications is null || applications.Count == 0)
            {
                throw new ObjectNotFoundInDataBaseException();
            }

            return applications;
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

        public void Add(Application application, string userName)
        {
            application.UserName = userName;
            if(application.Brawler is null)
            {
                application.Brawler = _brawlerRepository.Get(application.BrawlerName);
            }
            if(application.User is null)
            {
                application.User = _userRepository.Get(application.UserName);
            }
            _db.Application.Add(application);
            _db.SaveChanges();
        }
    }
}
