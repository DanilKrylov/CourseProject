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
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }
            ViewBag.UserName = ControllerContext.HttpContext.Session.GetString("name");
            var applicationList = _applicationRepository.GetAll();

            return View(applicationList);
        }


        public IActionResult Add()
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }
            
            ViewBag.Brawlers = _brawlerRepository.GetAll();

            ViewBag.UserName = ControllerContext.HttpContext.Session.GetString("name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Application application)
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }

            string userName = ControllerContext.HttpContext.Session.GetString("name");
            _applicationRepository.Add(application, userName);

            return RedirectPermanent("../Applications/All");
        }


        public IActionResult My()
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }
            IEnumerable userApplications;

            try
            {
                string userName = ControllerContext.HttpContext.Session.GetString("name");
                userApplications = _applicationRepository.GetUserApplications(userName);
            }
            catch(ObjectNotFoundInDataBaseException ex)
            {
                return View("../NotRegistered");
            }
            ViewBag.UserName = ControllerContext.HttpContext.Session.GetString("name");
            return View(userApplications);
        }

        public IActionResult Delete(int applicationId)
        {
            int Id = Convert.ToInt32(applicationId);
            #warning Сделать подтверждение пользователем
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }

            try
            {
                _applicationRepository.Delete(_applicationRepository.Get(Id), ControllerContext.HttpContext.Session.GetString("name"));
            }
            catch
            {
            }
            return RedirectPermanent("../Applications/All");
        }
    }
}
