using BSTeamSearch.Models;
using Microsoft.EntityFrameworkCore;
namespace BSTeamSearch.DataBase
{
    public class DBContent : DbContext
    {
        public DBContent(DbContextOptions<DBContent> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        public DbSet<Application> Application { get; set; }

        public DbSet<Brawler> Brawler { get; set; }
    }
}
