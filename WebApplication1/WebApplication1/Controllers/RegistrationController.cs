using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using BSTeamSearch.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSTeamSearch.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _userRepository;
        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel user)
        {
            if (!(user.Name is null) && AutorisationService.UserIsRegistered(_userRepository, user.Name))
            {
                ModelState.AddModelError("Name", "Данный логин уже занят, попробуйте какойто другой");
            }

            if (ModelState.IsValid)
            {
                _userRepository.Add(user);
                ControllerContext.HttpContext.Session.SetString("name", user.Name);
                
                return RedirectPermanent("../Applications/All");
            }

            return View(user);
        }
    }
}
