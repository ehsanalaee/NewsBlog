namespace NewsBlogWebPage.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


public class ArticleCreateEditVM
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; }

    public IFormFile? Image { get; set; }
    public string? ExistingImageName { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int WriterId { get; set; }
}