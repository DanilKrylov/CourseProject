using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BSTeamSearch.Repositories.Realisation
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DBContent _db;
        public LikeRepository(DBContent dataBase)
        {
            _db = dataBase;
        }

        public void AddLike(string userName, int applicationId)
        {
            if (!(_db.Likes.FirstOrDefault(c => c.UserName == userName && c.ApplicationId == applicationId) is null))
            {
                throw new Exception("Like already placed");
            }

            _db.Likes.Add(new Like(userName, applicationId));
            _db.SaveChanges();
        }

        public IEnumerable<Like> GetLikesForApplication(int applicationId)
        {
            var likes = _db.Likes.Where(c => c.ApplicationId == applicationId).ToList();
            return likes;
        }

        public IEnumerable<Like> GetLikesFromUser(string userName)
        {
            return _db.Likes.Where(c => c.UserName == userName)
                            .Include(c => c.Application)
                                .ThenInclude(c => c.Brawler);
        }

        public void RemoveLike(string userName, int applicationId)
        {
            var likeToRemove = _db.Likes.FirstOrDefault(c => c.UserName == userName && c.ApplicationId == applicationId);
            if (likeToRemove is null)
            {
                throw new Exception("Like is not in the database");
            }

            _db.Likes.Remove(likeToRemove);
            _db.SaveChanges();
        }
    }
}
