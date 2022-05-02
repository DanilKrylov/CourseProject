using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using BSTeamSearch.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSTeamSearch.Controllers
{
    public class AutorisationController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AutorisationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult AutorisationToTheSite()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AutorisationToTheSite(AutorisationViewModel user)
        {
            if(!(user.Name is null || user.Password is null) && 
               (!AutorisationService.UserIsRegistered(_userRepository, user.Name) ||
               !AutorisationService.PasswordIsCorrect(_userRepository, user.Name, user.Password)))
            {
                ModelState.AddModelError("Password", "Логгин или пароль указаны неправильно");
            }
            if (ModelState.IsValid)
            {
                ControllerContext.HttpContext.Session.SetString("name", user.Name);
                if (_userRepository.Get(user.Name).IsAdmin)
                {
                    return RedirectPermanent("../Admin/Index");
                }
                
                return RedirectPermanent("../Applications/All");
            }

            return View(user);
        }
    }
}
