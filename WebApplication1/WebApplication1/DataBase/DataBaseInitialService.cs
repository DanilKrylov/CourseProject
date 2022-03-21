using System.Collections.Generic;
using System.Linq;
using BSTeamSearch.Models;

namespace BSTeamSearch.DataBase
{
    public static class DataBaseInitialService
    {
        public static void InitialBrawlers(DBContent _content)
        {
            List<Brawler> _brawlers = new List<Brawler>()
            {
                new Brawler("Шелли", "../img/шелли.png"),
                new Brawler("Кольт", "../img/кольт.png"),
                new Brawler("Булл", "../img/булл.png"),
            };

            foreach(var brawler in _brawlers)
            {
                Brawler br = _content.Brawler.FirstOrDefault(c => c == brawler);
                if (br is null)
                {
                    _content.Brawler.Add(brawler);
                }
            }

            _content.SaveChanges();
            _content.Dispose();
        }
    }
}
