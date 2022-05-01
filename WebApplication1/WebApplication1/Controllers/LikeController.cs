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
        public LikeController(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }
        [HttpPost]
        public bool Like(string applicationId)
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return false;
            }
            string userName = ControllerContext.HttpContext.Session.GetString("name");
            _likeRepository.AddLike(userName, Convert.ToInt32(applicationId));
            return true;
        }

        [HttpPost] 
        public bool RemoveLike(string applicationId)
        {
            if (!ControllerContext.HttpContext.Session.Keys.Contains("name"))
            {
                return false;
            }

            string userName = ControllerContext.HttpContext.Session.GetString("name");
            _likeRepository.RemoveLike(userName, Convert.ToInt32(applicationId));
            return true;
        }
    }
}
