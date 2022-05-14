using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSTeamSearch.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        public LikeController(ILikeRepository likeRepository, IUserRepository userRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public bool Like(string applicationId)
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return false;
            }

            _likeRepository.AddLike(userName, Convert.ToInt32(applicationId));
            return true;
        }

        [HttpPost]
        public bool RemoveLike(string applicationId)
        {
            var userName = ControllerContext.HttpContext.Session.GetString("name");
            if (userName is null || !_userRepository.UserIsRegistered(userName))
            {
                return false;
            }

            _likeRepository.RemoveLike(userName, Convert.ToInt32(applicationId));
            return true;
        }
    }
}
