using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSTeamSearch.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBrawlerRepository _brawlerRepository;

        public ApplicationsController(IApplicationRepository applicationRepository, IUserRepository userRepository, IBrawlerRepository brawlerRepository )
        {
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
            _brawlerRepository = brawlerRepository;
        }
        public IActionResult All()
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return View("../NotRegistered");
            }
            ViewBag.UserName = userName;
            var applicationList = _applicationRepository.GetAllWithout(userName);
            return View(applicationList);
        }

        public IActionResult Add()
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return View("../NotRegistered");
            }

            ViewBag.Brawlers = _brawlerRepository.GetAll();

            ViewBag.UserName = userName;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Application application)
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return View("../NotRegistered");
            }
            ViewBag.UserName = userName;
            _applicationRepository.Add(application, userName);

            return RedirectPermanent("../Applications/All");
        }


        public IActionResult My()
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return View("../NotRegistered");
            }

            IEnumerable userApplications;
            ViewBag.UserName = userName;
            try
            {
                userApplications = _applicationRepository.GetUserApplications(userName);
            }
            catch(ObjectNotFoundInDataBaseException)
            {
                return View(null);
            }
            return View(userApplications);
        }

        public IActionResult DeleteApplication(int applicationId)
        {
            int Id = Convert.ToInt32(applicationId);
            #warning Сделать подтверждение пользователем
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return Redirect("../NotRegistered");
            }

            try
            {
                _applicationRepository.Delete(_applicationRepository.Get(Id), ControllerContext.HttpContext.Session.GetString("name"));
            }
            catch
            {
            }
            return RedirectPermanent("~/Applications/All");
        }

        [HttpPost]
        public IActionResult Filtration(bool onlyLiked, bool cupsAscending, string searchString, int minCups, int maxCups)
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return View("../NotRegistered");
            }

            ViewBag.UserName = userName;
            var applicationList = _applicationRepository.FiltrationGet(userName, onlyLiked, cupsAscending, searchString, minCups, maxCups).ToList();

            return View( applicationList);
        }
    }
}
