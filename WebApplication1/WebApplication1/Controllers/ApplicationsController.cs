using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BSTeamSearch.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly DBContent _dbContent;
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationsController(DBContent db, IApplicationRepository applicationRepository)
        {
            _dbContent = db;
            _applicationRepository = applicationRepository;
        }



        public IActionResult All()
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }
            ViewBag.UserName = ControllerContext.HttpContext.Session.GetString("name");
            var applicationList = _applicationRepository.GetAllApplications();

            return View(applicationList);
        }


        public IActionResult Add()
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }
            //
            SelectList brawlerListItems = new SelectList(_dbContent.Brawler,"Name", "Name");
            ViewBag.BrawlerListItems = brawlerListItems;
            
            ViewBag.Brawlers = _dbContent.Brawler.ToList();
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

            #warning запихнуть добавление в ApplicationRepository
            User user = _dbContent.User.First(c => c.Name == userName);

            application.Brawler = _dbContent.Brawler.First(c => c.Name == application.BrawlerName);
            user.Applications.Add(application);
            //_dbContent.Application.Add(application);

            _dbContent.SaveChanges();
            return View("../Applications/All");
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
                
            return View(userApplications);
        }

        
        public IActionResult Delete(string applicationId)
        {
            int Id = Convert.ToInt32(applicationId);
            #warning Сделать подтверждение пользователем
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return View("../NotRegistered");
            }

            try
            {
                _applicationRepository.Delete(_applicationRepository.GetApplication(Id), ControllerContext.HttpContext.Session.GetString("name"));
            }
            catch
            {
                return RedirectPermanent("../Applications/All");
            }
            return RedirectPermanent("../Applications/All");
        }
    }
}
