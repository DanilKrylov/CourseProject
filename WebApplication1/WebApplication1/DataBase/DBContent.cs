using System.IO;
using System.Text.Json;
using BSTeamSearch.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BSTeamSearch.DataBase
{
    public class DBContent : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Application> Application { get; set; }

        public DbSet<Brawler> Brawler { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Message> Messages { get; set; }
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
                    new Brawler("Бо", "../img/бо.png"),
                    new Brawler("Джин", "../img/джин.png"),
                    new Brawler("Пэм", "../img/пэм.png"),
                    new Brawler("Роза", "../img/роза.png"),
                });

            User[] admins = JsonConvert.DeserializeObject<User[]>(File.ReadAllText("admins.json"));
            modelBuilder.Entity<User>().HasData(admins);
        }
    }
}
