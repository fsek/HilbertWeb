namespace HilbertOS.BackendApp.Models
{
    public class NewsPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
