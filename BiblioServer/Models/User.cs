namespace BiblioServer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string? Bio { get; set; }

    public string? Avatar { get; set; }
    
    public string? Password { get; set; }
    
    public string? Salt { get; set; }
    
    public bool? IsAdmin { get; set; }
    
    //public ICollection<Book> FavoriteBooks { get; set; } = new List<Book>();
}