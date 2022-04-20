using BSTeamSearch.Models;
using Microsoft.EntityFrameworkCore;
namespace BSTeamSearch.DataBase
{
    public class DBContent : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Application> Application { get; set; }

        public DbSet<Brawler> Brawler { get; set; }

        public DbSet<Discord> Discords { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DBContent(DbContextOptions<DBContent> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brawler>().HasData(
                new Brawler[]
                {
                    new Brawler("Шелли", "../img/шелли.png"),
                    new Brawler("Кольт", "../img/кольт.png"),
                    new Brawler("Булл", "../img/булл.png"),
                });
        }
    }
}
