using System.ComponentModel.DataAnnotations;

namespace BiblioServer.Models;

public class Comment
{
   
    //Ключ комментария
    [Key]
    public int Id { get; set; }

    //Контент комментария,его содержание
    [Required(ErrorMessage = "Comment content is required.")]
    [MaxLength(500, ErrorMessage = "Comment content cannot exceed 500 characters.")]
    public string Content { get; set; }

    //Оценка 
    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
    public int Rating { get; set; }
    
    //Относится к пользователю, который оставил комментарий
    public User User { get; set; }

    //Относится к книге, к которой оставили комментарий
    public Book Book { get; set; }
}