using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;
using Microsoft.EntityFrameworkCore;
namespace BSTeamSearch.DataBase
{
    public class DBContent : DbContext
    {
        public DBContent(DbContextOptions<DBContent> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public DbSet<User> User { get; set; }
        public DbSet<Application> Application { get; set; }

        public DbSet<Brawler> Brawler { get; set; }
    }
}
