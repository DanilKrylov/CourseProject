using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BSTeamSearch.Annotations;

namespace BSTeamSearch.Models
{
    public class User
    {
        [Key]
        public string Name { get; set; }
        public string Password { get; set; }

        public Discord Discord { get; set; }

        public bool IsAdmin { get; set; } = false;

        public List<Application> Applications { get; set; } = new();

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
