using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface ILikeRepository
    {
        public void AddLike(string userName, int applicationId);

        public void RemoveLike(string userName, int applicationId);

        public IEnumerable<Like> GetLikesForApplication(int applicationId);

        public IEnumerable<Like> GetLikesFromUser(string userName);
    }
}
