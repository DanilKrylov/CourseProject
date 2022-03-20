using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;

namespace BSTeamSearch.Services
{
    public static class Brawlers
    {
        private static Dictionary<BrawlersEnum, Brawler> _brawlers = new Dictionary<BrawlersEnum, Brawler>();

        public static Brawler GetBrawlerName(BrawlersEnum Brawler)
        {
            if (!_brawlers.Keys.Contains(Brawler))
            {
                throw new KeyNotFoundException();
            }
            return _brawlers[Brawler];
        }

        public static Dictionary<BrawlersEnum, Brawler> GetAllBrawlers()
        {
            var _dict = new Dictionary<BrawlersEnum, Brawler>();

            foreach(KeyValuePair<BrawlersEnum, Brawler> keyValuePair in _brawlers)
            {
                _dict.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return _dict;
        }
    }
}
