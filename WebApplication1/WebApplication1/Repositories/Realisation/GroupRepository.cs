using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Models;
using BSTeamSearch.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BSTeamSearch.Repositories.Realisation
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DBContent _db;
        public GroupRepository(DBContent dataBase)
        {
            _db = dataBase;
        }
        public void CreateGroup(string firstUserName, string secondUserName)
        {
            if(firstUserName is null || secondUserName is null)
            {
                throw new ArgumentNullException();
            }
            var group = new Group(firstUserName, secondUserName);
            _db.Groups.Add(group);
            _db.SaveChanges();
        }


        public Group GetGroup(int groupId)
        {
            var group = _db.Groups.Include(c => c.Messages).FirstOrDefault(c => c.Id == groupId);
            if (group is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }
            return group;
        }
        public Group GetGroup(string firstUserName, string secondUserName)
        {
            var group = _db.Groups.Include(c => c.Messages).FirstOrDefault(c => (c.FirstUserName == firstUserName &&
                                                       c.SecondUserName == secondUserName) ||
                                                       (c.FirstUserName == secondUserName &&
                                                       c.SecondUserName == firstUserName));
            if (group is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }
            return group;
        }

        public List<Group> GetGroupsForUser(string userName)
        {
            var groups = _db.Groups.Where(c => c.FirstUserName == userName || c.SecondUserName == userName)
                                   .Where(c => c.FirstUserName != c.SecondUserName)
                                   .Include(c => c.Messages).ToList();
            return groups;
        }


        public bool GroupIsCreated(int groupId)
        {
            var group = _db.Groups.FirstOrDefault(c => c.Id == groupId);
            if (group is null)
            {
                return false;
            }

            return true;
        }
        public bool GroupIsCreated(string firstUserName, string secondUserName)
        {
            var group = _db.Groups.FirstOrDefault(c => (c.FirstUserName == firstUserName &&
                                                       c.SecondUserName == secondUserName) ||
                                                       (c.FirstUserName == secondUserName &&
                                                       c.SecondUserName == firstUserName));
            if (group is null)
            {
                return false;
            }

            return true;
        }
    }
}
