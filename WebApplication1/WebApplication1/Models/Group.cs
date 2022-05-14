using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSTeamSearch.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string FirstUserName { get; set; }
        public User FirstUser { get; set; }

        public string SecondUserName { get; set; }
        public User SecondUser { get; set; }

        public List<Message> Messages { get; set; } = new ();

        public Group(string firstUserName, string secondUserName)
        {
            FirstUserName = firstUserName;
            SecondUserName = secondUserName;
        }

        public Message GetLastMessage()
        {
            if (Messages.Count == 0)
            {
                throw new Exception("NoMessagesOnGroup");
            }

            Message lastMessage = Messages.FirstOrDefault();
            foreach (Message message in Messages)
            {
                if (lastMessage.Time < message.Time)
                {
                    lastMessage = message;
                }
            }

            return lastMessage;
        }
    }
}
