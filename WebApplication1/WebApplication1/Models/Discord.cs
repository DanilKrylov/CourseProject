using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSTeamSearch.Models
{
    public class Discord
    {
        [Key]
        public string UserName { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public int DiscordSharp { get; set; }

        

        public Discord(string name, int discordSharp, string userName)
        {
            Name = name;
            DiscordSharp = discordSharp;
            UserName = userName;
        }
    }
}
