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
            _userRepository.ChecklUniquenessOfModel(user, ModelState);

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
