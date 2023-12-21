using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BiblioServer.Models;

public class UserRegistrationModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$", ErrorMessage = "Пароль должен содержать хотя бы одну букву и одну цифру")]
    [MinLength(8, ErrorMessage = "Пароль должен содержать не менее 8 символов")]
    public string Password { get; set; }
}

