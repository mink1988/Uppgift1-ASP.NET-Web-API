using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedRepository.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssuesController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IConfiguration _configuration;

        public IssuesController(SqlDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIssueModel model)
        {
            try
            {
                var issue = new Issue
                {
                    Customer = model.Customer,
                    Description = model.Description,
                    Created = model.Created,
                    IssueStatus = model.IssueStatus,
                    Completed = model.Completed,
                    UserId = int.Parse(User.FindFirst("UserId").Value),
                };

                _context.Issues.Add(issue);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex) { return new BadRequestObjectResult(ex.Message); }

            return new OkResult();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues([FromQuery] string customer, [FromQuery] string issueStatus, [FromQuery] DateTime? created)
        {
            var query = _context.Issues.AsQueryable();

            if (!string.IsNullOrEmpty(customer))
            {
                query = query.Where(x => x.Customer == customer);
            }
            if (!string.IsNullOrEmpty(issueStatus))
            {
                query = query.Where(x => x.IssueStatus == issueStatus);
            }
            if (created.HasValue)
            {
                query = query.Where(x => x.Created.Date == created.Value.Date);
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Issue>> GetIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);


            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssue(int id, Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest();
            }

            _context.Entry(issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
