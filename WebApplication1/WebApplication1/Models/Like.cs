using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSTeamSearch.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public Like(string userName, int applicationId)
        {
            UserName = userName;
            ApplicationId = applicationId;
        }
    }
}
