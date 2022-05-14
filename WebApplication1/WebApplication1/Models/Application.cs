using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSTeamSearch.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        public int CountOfCups { get; set; }

        public string UserName { get; set; }
        public User User { get; set; }

        public string BrawlerName { get; set; }
        public Brawler Brawler { get; set; }

        public bool HasVoiceChat { get; set; }

        public string Description { get; set; }

        public List<Like> Likes { get; set; }

        public Application(int countOfCups, string userName, string brawlerName, bool hasVoiceChat, string description)
        {
            CountOfCups = countOfCups;
            UserName = userName;
            BrawlerName = brawlerName;
            HasVoiceChat = hasVoiceChat;
            Description = description;
        }
    }
}
