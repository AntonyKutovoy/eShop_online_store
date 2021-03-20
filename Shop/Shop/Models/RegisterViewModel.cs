using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "Поле email не заполнено")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароль не заполнено")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле подтвердить пароль не заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
