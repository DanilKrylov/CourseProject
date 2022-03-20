using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSTeamSearch.Models
{
    public class Application
    {
        public int Id { get; set; }


        public string BrawlerName { get; set; }
        public Brawler Brawler { get; set; }

        public int CountOfCups { get; set; }

        public User User { get; set; }

        
        /*public Application(Brawler brawler, int countOfCups)
        {
            UserBrawler = brawler;

            CountOfCups = countOfCups;
        }*/
    }
}
