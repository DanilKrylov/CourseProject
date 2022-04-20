using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Models;

namespace BSTeamSearch.ViewModels
{
    public class AutorisationViewModel
    {
        [Required(ErrorMessage = "Поле логгин не может быть пустым")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле пароля не может быть пустым")]
        public string Password { get; set; }
    }
}
