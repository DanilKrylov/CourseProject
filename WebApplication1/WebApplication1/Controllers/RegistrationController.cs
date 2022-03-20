using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using BSTeamSearch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSTeamSearch.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly DBContent _dbContent;
        public RegistrationController(DBContent db)
        {
            _dbContent = db;
        }
        public IActionResult Registration()
        {

            if (ControllerContext.HttpContext.Session.GetString("name") != null)
            {
                _dbContent.Dispose();
                return View("../Applications/All");
            }
            
            return View();
        }
        [HttpPost]
        public IActionResult Registration([Bind(include: "Name,Password")] User user)
        {
#warning TO DO:
            if (!(user.Name is null) && AutorisationService.UserIsRegistered(_dbContent, user))
            {
                ModelState.AddModelError("Name", "Данный логин уже занят, попробуйте какойто другой");
            }

            if (ModelState.IsValid)
            {
                _dbContent.User.Add(user);
                _dbContent.SaveChanges();
                ControllerContext.HttpContext.Session.SetString("name", user.Name);
                
                return View("../Applications/All");
                
            }

            return View(user);
        }
    }
}
