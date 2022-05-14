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

        public bool IsAdmin { get; set; } = false;

        public int Age { get; set; }

        public string Email { get; set; }

        public int CountOfCups { get; set; }

        public string BrawlAccountName { get; set; }

        public bool IsBanned { get; set; } = false;

        public List<Application> Applications { get; set; } = new ();

        public User(string name, string password, int age, string email, int countOfCups, string brawlAccountName)
        {
            Name = name;
            Password = password;
            Age = age;
            Email = email;
            CountOfCups = countOfCups;
            BrawlAccountName = brawlAccountName;
        }
    }
}
