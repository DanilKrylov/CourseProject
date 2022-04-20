using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BSTeamSearch.Annotations;
using BSTeamSearch.Models;

namespace BSTeamSearch.ViewModels
{
    public class RegistrationViewModel
    {
        [BanSpacedInString(ErrorMessage = "Недопустимы пробелы в логине")]
        [Required(ErrorMessage = "Поле логгин не может быть пустым")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 12 символов")]
        public string Name { get; set; }

        [BanSpacedInString(ErrorMessage = "Недопустимы пробелы в пароле")]
        [Required(ErrorMessage = "Поле пароля не может быть пустым")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 12 символов")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Поле пароля и поле подтверждения пароля должны совпадать")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Поле не может быть пустым")]
        public string DiscordName { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Range(1000,9999, ErrorMessage = "Длина должна быть равной четырем символам")]
        public int DiscordSharp { get; set; }
    }
}
