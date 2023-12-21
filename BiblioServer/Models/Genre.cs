using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioServer.Models;

public class Genre
{
    //ключ Жанра
    [Key]
    public int Id { get; set; }


    //Жанр
    [Required(ErrorMessage = "Genre name is required.")]
    [MaxLength(50, ErrorMessage = "Genre name cannot be more than 50 characters.")]
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Genre name can only contain letters and spaces.")]
    public string Title { get; set; }
}