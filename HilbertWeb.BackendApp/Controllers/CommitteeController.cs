using HilbertWeb.BackendApp.Database;
using HilbertWeb.BackendApp.Dto;
using HilbertWeb.BackendApp.Dto.Permissions;
using HilbertWeb.BackendApp.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HilbertWeb.BackendApp.Controllers;
[Route("api/committee")]
[ApiController]
public class CommitteeController : ControllerBase
{
    private readonly ILogger<NewsController> _logger;
    private readonly ApplicationDbContext _db;

    public CommitteeController(ILogger<NewsController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    [Authorize(Policy = "Permissions.Committees.View")]
    public async Task<ActionResult<IEnumerable<CommitteeDto>>> Get()
    {
        var committees = await _db.Committees.OrderByDescending(x => x.Id).ToListAsync();

        return Ok(committees.Adapt<List<CommitteeDto>>());
    }

    [HttpPost]
    public async Task<ActionResult> Post(ManageCommitteeDto model)
    {
        Committee dbModel = new Committee();
        dbModel.Name = model.Name;

        await _db.Committees.AddAsync(dbModel);
        await _db.SaveChangesAsync();

        return Created("/", dbModel.Adapt<CommitteeDto>());
    }

    [HttpPut]
    public async Task<ActionResult> Update(ManageCommitteeDto model)
    {
        var committee = await _db.Committees.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        if (committee == null)
            return Unauthorized();

        committee.Name = model.Name;
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("asdf")]
    public async Task<ActionResult> Delete(ManageCommitteeDto model)
    {
        var committee = await _db.Committees.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        if (committee == null)
            return Unauthorized();

        _db.Committees.Remove(committee);
        await _db.SaveChangesAsync();

        return Ok();
    }
}
