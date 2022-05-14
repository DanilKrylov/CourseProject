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
        [Required(ErrorMessage = "Поле возраста не может быть пустым")]
        [Range(7, 100, ErrorMessage ="Допустимое значение для возраста от 7 до 100 лет")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Поле почты не может быть пустым")]
        [StringLength(25, ErrorMessage = "Максимальная длина почты 25 смволов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле общего количества кубков не может быть пустым")]
        [Range(0, 50000, ErrorMessage ="Допустимое значение общего количества кубков от 0 до 50.000 ")]
        public int CountOfCups { get; set; }

        [Required(ErrorMessage = "Поле никнейма не может быть пустым")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 12 символов")]
        public string BrawlAccountName { get; set; }
    }
}
