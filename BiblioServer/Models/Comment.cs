using System.ComponentModel.DataAnnotations;

namespace BiblioServer.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public int Rating { get; set; }
    
    public User User { get; set; }
    
    public Book Book { get; set; }
}