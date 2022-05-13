using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        public bool GroupIsCreated(int id);
        public bool GroupIsCreated(string firstUserName, string secondUserName);

        public void CreateGroup(string firstUserName, string secondUserName);

        public Group GetGroup(int id);
        public Group GetGroup(string firstUserName, string secondUserName);

        public List<Group> GetGroupsForUser(string userName);
    }
}
