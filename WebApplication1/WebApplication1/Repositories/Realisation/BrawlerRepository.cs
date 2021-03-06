using System.Collections;
using System.Linq;
using BSTeamSearch.DataBase;
using BSTeamSearch.Models;
using BSTeamSearch.Exceptions;
using BSTeamSearch.Repositories.Interfaces;

namespace BSTeamSearch.Repositories.Realisation
{
    public class BrawlerRepository : IBrawlerRepository
    {
        private readonly DBContent _db;
        public BrawlerRepository(DBContent DataBase)
        {
            _db = DataBase;
        }
        public IEnumerable GetAll()
        {
            var brawlers =  _db.Brawler.ToList();
            return brawlers;
        }

        public Brawler Get(string brawlerName)
        {
            var brawler = _db.Brawler.FirstOrDefault(c => c.Name == brawlerName);

            if(brawler is null)
            {
                throw new ObjectNotFoundInDataBaseException();
            }
            return brawler;
        }

        public void Add(Brawler brawler)
        {
            _db.Brawler.Add(brawler);
            _db.SaveChanges();
        }
    }
}
