using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSTeamSearch.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;

        public AdminController(IUserRepository userRepository, IApplicationRepository applicationRepository)
        {
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
        }

        public IActionResult Index()
        {
            string userName = HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return View("../NotRegistered");
            }

            if (!_userRepository.Get(userName).IsAdmin)
            {
                return RedirectToAction("Applications/All");
            }

            return View();
        }

        [HttpPost]
        public IActionResult GetApplications(bool cupsAscending, string searchString, int minCups, int maxCups)
        {
            string userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName) || !_userRepository.Get(userName).IsAdmin)
            {
                return null;
            }

            ViewBag.UserName = userName;
            var applicationList = _applicationRepository.FiltrationGet(userName, false, cupsAscending, searchString, minCups, maxCups).ToList();

            return View(applicationList);
        }

        [HttpPost]
        public bool RemoveApplication(int id, string userName)
        {
            var name = HttpContext.Session.GetString("name");
            if (userName is null || userName == name || !_userRepository.UserIsRegistered(name) || !_userRepository.Get(name).IsAdmin)
            {
                return false;
            }

            try
            {
                _applicationRepository.Delete(_applicationRepository.Get(id), userName);
            }
            catch
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public IActionResult GetUsers(string searchString)
        {
            var userName = HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName) || !_userRepository.Get(userName).IsAdmin)
            {
                return View();
            }

            return View(_userRepository.GetAll(searchString));
        }

        public IActionResult GetUserData(string userName)
        {
            var name = HttpContext.Session.GetString("name");
            if (userName is null || userName == name || !_userRepository.UserIsRegistered(name) || _userRepository.Get(userName).IsAdmin)
            {
                return View();
            }

            return View(_userRepository.Get(userName));
        }

        [HttpPost]
        public bool BanUser(string userName)
        {
            var adminName = HttpContext.Session.GetString("name");
            if (adminName is null || !_userRepository.UserIsRegistered(adminName) ||
                !_userRepository.UserIsRegistered(adminName) || userName == adminName)
            {
                return false;
            }

            _userRepository.BanUser(userName);
            return true;
        }

        [HttpPost]
        public bool UnbanUser(string userName)
        {
            var adminName = HttpContext.Session.GetString("name");
            if (adminName is null || !_userRepository.UserIsRegistered(adminName) ||
                !_userRepository.UserIsRegistered(adminName) || userName == adminName)
            {
                return false;
            }

            _userRepository.UnbanUser(userName);
            return true;
        }
    }
}
