using HilbertWeb.BackendApp.Database;
using HilbertWeb.BackendApp.Models;
using HilbertWeb.BackendApp.Models.Identity;
using HilbertWeb.BackendApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HilbertWeb.BackendApp.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewsController(ILogger<NewsController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<NewsPost>> Get()
        {
            return await _db.NewsPosts.Include(news => news.Author).OrderByDescending(x => x.Created).ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _db.NewsPosts.Where(x => x.Id == id).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(ManageNewsViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var news = new NewsPost
            {
                Title = model.Title,
                Content = model.Content,
                Author = currentUser,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };
            
            _db.NewsPosts.Add(news);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ManageNewsViewModel model)
        {
            var news = _db.NewsPosts.Where(x => x.Id == model.Id).FirstOrDefault();
            if (news == null)
                return BadRequest();

            news.Title = model.Title;
            news.Content = model.Content;
            news.Updated = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ManageNewsViewModel model)
        {
            var news = _db.NewsPosts.Where(x => x.Id == model.Id).FirstOrDefault();
            if (news == null)
                return BadRequest();

            _db.NewsPosts.Remove(news);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}