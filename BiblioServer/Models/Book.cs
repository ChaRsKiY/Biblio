using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioServer.Models;

public class Book
{
    //Ключ книги
    [Key]
    public int Id { get; set; }

    //Название книги
    [Required]
    [MaxLength(60)]
    public string Title { get; set; }

    //Автор книги
    [Required]
    [MaxLength(60)]
    public string Author { get; set; }

    //Жанр книги
    [ForeignKey("Genre")]
    public Genre Genre { get; set; }

    //Путь к обложке книги
    public string CoverImage { get; set; }

    //Описание книги
    [MaxLength(255)]
    public string Description { get; set; }

    //Оценка
    [Range(0, 10)]
    public double Rating { get; set; }

    //Счетчик прочтений книги
    public int ReadCounter { get; set; }

    //Счетчик загрузок книги
    public int DownloadCount { get; set; }

    //Дата публикации книги
    [DataType(DataType.Date)]
    public DateOnly PublicationDate { get; set; }

    //Год выпуска книги
    [DataType(DataType.Date)]
    public DateOnly Year { get; set; }

    //Содержание книги(Не описание)
    public string Content { get; set; }
}