using HilbertWeb.BackendApp.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace HilbertWeb.BackendApp.Models;

public class NewsPost
{
    public int Id { get; set; }

    [MaxLength(70)]
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    [Required]
    public int AuthorId { get; set; }
    public ApplicationUser Author { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
