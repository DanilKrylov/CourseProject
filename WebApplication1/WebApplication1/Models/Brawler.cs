using System.ComponentModel.DataAnnotations;

namespace BSTeamSearch.Models
{
    public class Brawler
    {
        [Key]
        public string Name { get; set; }

        public string Img { get; set; }
        public Brawler(string name, string img)
        {
            Name = name;
            Img = img;
        }
    }
}