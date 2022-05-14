using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;

namespace BSTeamSearch.ViewModels
{
    public class ApplicationAddViewModel
    {
        [Required(ErrorMessage ="Поле не можно быть пустым")]
        [Range(0, 1250, ErrorMessage ="количество кубков должно быть от 0 до 1250")]
        public int CountOfCups { get; set; }

        public string BrawlerName { get; set; }

        public bool HasVoiceChat { get; set; }

        [Required(ErrorMessage ="Поле не можно быть пустым")]
        [StringLength(40, MinimumLength = 10, ErrorMessage ="Длина описания должна быть от 10 до 40 символов")]
        public string? Description { get; set; }

        public Application GetApplicationModel(string userName)
        {
            return new Application(CountOfCups, userName, BrawlerName, HasVoiceChat, Description);
        }
    }
}
