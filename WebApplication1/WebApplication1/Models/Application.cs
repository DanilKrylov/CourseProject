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
    }
}
