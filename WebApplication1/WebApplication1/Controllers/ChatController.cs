using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSTeamSearch.Controllers
{
    public class ChatController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        public ChatController(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return Redirect("../NotRegistered");
            }

            var groups = _groupRepository.GetGroupsForUser(userName);
            ViewBag.UserName = userName;
            return View(groups);
        }

        [HttpGet]
        public IActionResult SendMessage(string toUser)
        {
            string userName = HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return Redirect("../NotRegistered");
            }

            ViewBag.UserName = userName;
            if (userName == toUser || !_userRepository.UserIsRegistered(toUser))
            {
                return View("../Chat/Index", _groupRepository.GetGroupsForUser(userName));
            }

            if (!_groupRepository.GroupIsCreated(userName, toUser))
            {
                _groupRepository.CreateGroup(userName, toUser);
            }

            ViewBag.CurrentChat = _groupRepository.GetGroup(userName, toUser);
            return View("../Chat/Index", _groupRepository.GetGroupsForUser(userName));
        }

        [HttpPost]
        public IActionResult GetChat(int chatId)
        {
            var userName = HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return Redirect("../NotRegistered");
            }

            if (!_groupRepository.GroupIsCreated(chatId))
            {
                return null;
            }

            ViewBag.UserName = userName;
            return View(_groupRepository.GetGroup(chatId));
        }
    }
}
