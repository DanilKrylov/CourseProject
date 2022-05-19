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
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBrawlerRepository _brawlerRepository;
        public ApplicationRepository(DBContent dataBase, IUserRepository userRepository, IBrawlerRepository brawlerRepository, ILikeRepository likeRepository)
        {
            _db = dataBase;
            _userRepository = userRepository;
            _brawlerRepository = brawlerRepository;
            _likeRepository = likeRepository;
        }

        public Application Get(int applicationId)
        {
            var application = _db.Application.Include(c => c.Brawler)
                                  .Include(c => c.User)
                                  .FirstOrDefault(c => c.Id == applicationId);

            if (application is null)
            {
                _db.Dispose();
                throw new ObjectNotFoundInDataBaseException();
            }

            application.Likes = _likeRepository.GetLikesForApplication(application.Id).ToList();
            return application;
        }

        public IEnumerable<Application> GetAllWithout(string userNameIgnore)
        {
            var applications = _db.Application.Where(c => c.UserName != userNameIgnore)
                                              .Include(c => c.Brawler)
                                              .Include(c => c.User)
                                              .ToList();
            foreach (var application in applications)
            {
                application.Likes = _likeRepository.GetLikesForApplication(application.Id).ToList();
            }

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

            foreach (var application in applications)
            {
                application.Likes = _likeRepository.GetLikesForApplication(application.Id).ToList();
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

            _db.Likes.RemoveRange(_likeRepository.GetLikesForApplication(application.Id));
            _db.Application.Remove(application);
            user.Applications.Remove(application);
            _db.SaveChanges();
        }

        public void Add(Application application)
        {
            if (application.Brawler is null)
            {
                application.Brawler = _brawlerRepository.Get(application.BrawlerName);
            }

            if (application.User is null)
            {
                application.User = _userRepository.Get(application.UserName);
            }

            _db.Application.Add(application);
            _db.SaveChanges();
        }

        public IEnumerable<Application> FiltrationGet(string userName, bool onlyLiked, bool cupsAscending, string searchString, int minCups, int maxCups)
        {
            List<Application> applications = new List<Application>();
            if (onlyLiked)
            {
                foreach (Like like in _likeRepository.GetLikesFromUser(userName))
                {
                    applications.Add(Get(like.ApplicationId));
                }
            }
            else
            {
                applications = GetAllWithout(userName).ToList();
            }

            if (searchString is not null)
            {
                searchString = searchString.ToLower();
                applications = applications.Where(c => c.BrawlerName.ToLower().Contains(searchString) ||
                                           c.UserName.ToLower().Contains(searchString)).ToList();
            }

            applications = applications.Where(c => c.CountOfCups >= minCups && c.CountOfCups <= maxCups).OrderBy(c => c.CountOfCups).ToList();
            if (!cupsAscending)
            {
                applications.Reverse();
            }

            return applications;
        }

        public void Edit(Application newApplication)
        {
            var application = Get(newApplication.Id);

            application.Description = newApplication.Description;
            application.BrawlerName = newApplication.BrawlerName;
            application.CountOfCups = newApplication.CountOfCups;
            application.HasVoiceChat = newApplication.HasVoiceChat;

            _db.SaveChanges();
        }
    }
}
