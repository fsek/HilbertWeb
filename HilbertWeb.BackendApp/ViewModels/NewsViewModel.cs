using HilbertWeb.BackendApp.ViewModels.Permissions;

namespace HilbertWeb.BackendApp.ViewModels;

public class NewsPostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public UserViewModel Author { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
