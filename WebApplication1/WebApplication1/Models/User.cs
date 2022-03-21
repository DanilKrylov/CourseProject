using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BSTeamSearch.Annotations;

namespace BSTeamSearch.Models
{
    public class User
    {
        [Key]
        [BanSpacedInString(ErrorMessage = "Недопустимы пробелы в логине")]
        [Required(ErrorMessage = "Поле логгин не может быть пустым")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 12 символов")]
        public string Name { get; set; }

        [BanSpacedInString(ErrorMessage = "Недопустимы пробелы в пароле")]
        [Required(ErrorMessage = "Поле пароля не может быть пустым")]
        [Display(Name = "пароль")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 12 символов")]
        public string Password { get; set; }


        public List<Application> Applications { get; set; } = new();
    }
}
