using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSTeamSearch.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public string UserName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public Message(string text, int groupId, string userName)
        {
            Text = text;
            GroupId = groupId;
            UserName = userName;
            Time = DateTime.Now;
        }
    }
}
