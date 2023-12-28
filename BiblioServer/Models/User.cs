namespace BiblioServer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

public class User
{
    [Key]
    public int Id { get; set; }
    //Уникальное имя пользователя.
    [Required]
    [StringLength(15, MinimumLength = 3)]
    [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
    public string UserName { get; set; }

    //Электронная почта пользователя.
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    //Краткая биография или описание.
    [MaxLength(500)]
    public string? Bio { get; set; }

    //Путь на изображение аватара пользователя.
    public string? Avatar { get; set; }

    //Хешированый пароль пользователя.
    public string? HashedPassword { get; set; }

    //Соль для хеширования пароля.
    public string? Salt { get; set; }

    //Флаг, указывающий, является ли пользователь администратором.
    public bool? IsAdmin { get; set; }

    //Коллекция любимых книг пользователя.
    //public ICollection<Book> FavoriteBooks { get; set; } = new List<Book>();
}