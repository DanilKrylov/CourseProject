using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.DataBase;
using BSTeamSearch.Models;

namespace BSTeamSearch.Services
{
    public static class DataBaseInitialService
    {
        public static void InitialBrawlers(DBContent _content)
        {
            Dictionary<Brawler, BrawlersEnum> _brawlers = new Dictionary<Brawler, BrawlersEnum>()
            {
                { new Brawler("Шелли", "../img/шелли.png"), BrawlersEnum.Shelly },
                { new Brawler("Кольт", "../img/кольт.png"), BrawlersEnum.Colt },
                { new Brawler("Булл", "../img/булл.png"), BrawlersEnum.Bull },
            };

            var _brawlersDict = Brawlers.GetAllBrawlers();
            foreach(var keyValuePair in _brawlers)
            {
                Brawler br = _content.Brawler.FirstOrDefault(c => c == keyValuePair.Key);
                if (br is null)
                {
                    _content.Brawler.Add(keyValuePair.Key);
                }

                if (!_brawlersDict.Values.Contains(keyValuePair.Key))
                {
                    _brawlersDict.Add(keyValuePair.Value, keyValuePair.Key);
                }
            }
            _content.SaveChanges();

            
        }
    }
}
