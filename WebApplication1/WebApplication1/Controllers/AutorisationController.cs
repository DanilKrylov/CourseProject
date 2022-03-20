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
using Microsoft.Extensions.DependencyInjection;

namespace BSTeamSearch.Controllers
{
    public class AutorisationController : Controller
    {
        #warning добавить страницу при переходе на регистрацию , если в куки есть name
        private readonly DBContent _dbContent;
        public AutorisationController(DBContent db)
        {
            _dbContent = db;
        }

        

        public IActionResult AutorisationToTheSite()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AutorisationToTheSite([Bind(include: "Name,Password")] User user)
        {
            if(!(user.Name is null || user.Password is null) && !AutorisationService.PasswordIsCorrect(_dbContent, user))
            {
                ModelState.AddModelError("Password", "Логгин или пароль указаны неправильно");
            }

            if (ModelState.IsValid)
            {
                _dbContent.Dispose();
                ControllerContext.HttpContext.Session.SetString("name", user.Name);
                return RedirectPermanent("../Applications/All");
            }

            return View(user);
        }
    }
}
