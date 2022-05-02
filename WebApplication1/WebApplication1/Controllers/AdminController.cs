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
            string name = HttpContext.Session.GetString("name");
            if(name is null)
            {
                return View("../NotRegistered");
            }

            if (!_userRepository.Get(name).IsAdmin)
            {
                return RedirectToAction("Applications/All");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetApplications(bool onlyLiked, bool cupsAscending, string searchString, int minCups, int maxCups)
        {
            string userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.Get(userName).IsAdmin)
            {
                return null;
            }
            
            ViewBag.UserName = userName;
            var applicationList = _applicationRepository.FiltrationGet(userName, onlyLiked, cupsAscending, searchString, minCups, maxCups).ToList();

            return View(applicationList);
        }
        [HttpPost]
        public bool RemoveApplication(int id, string userName)
        {
            var name = HttpContext.Session.GetString("name");
            if ( name is null || !_userRepository.Get(name).IsAdmin)
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
            var name = HttpContext.Session.GetString("name");
            if (name is null || !_userRepository.Get(name).IsAdmin)
            {
                return View();
            }
            return View(_userRepository.GetAll(searchString));
        }

        public IActionResult GetUserData(string userName)
        {
            var name = HttpContext.Session.GetString("name");
            if (name is null || !_userRepository.Get(name).IsAdmin)
            {
                return View();
            }
            return View(_userRepository.Get(userName));
        }
    }
}
