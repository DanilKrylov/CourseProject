using System.Collections;
using BSTeamSearch.Models;

namespace BSTeamSearch.Repositories.Interfaces
{
    public interface IBrawlerRepository
    {
        public void Add(Brawler brawler);
        public IEnumerable GetAll();

        public Brawler Get(string brawlerName);
    }
}
