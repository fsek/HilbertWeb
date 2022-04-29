using HilbertOS.BackendApp.Database;
using HilbertOS.BackendApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HilbertOS.BackendApp.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly ApplicationDbContext _db;

        public NewsController(ILogger<NewsController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<NewsPost> Get()
        {
            return _db.NewsPosts.OrderByDescending(x => x.Created);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var news = new NewsPost { Title = "Dabdabdab", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", Created = DateTime.UtcNow, Updated = DateTime.UtcNow };
            _db.NewsPosts.Add(news);
            _db.NewsPosts.RemoveRange(_db.NewsPosts);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = news.Id }, news);
        }
    }
}