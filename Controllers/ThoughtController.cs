using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dream.Models;
using Dream.Data;
#nullable enable

namespace Dream.Controllers;

[ApiController]
[Route("[controller]")]
public class ThoughtController : ControllerBase
{

    private readonly DreamDbContext context;

    public ThoughtController(DreamDbContext _context)
    {
        context = _context;
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<List<Thought>>> GetAllThoughts()
    {
        List<Thought> thoughts = await context.Thoughts.ToListAsync();
        return thoughts;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Thought>> GetThought(int id)
    {
        Thought? thought = await context.Thoughts.FindAsync(id);

        if (thought == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(thought);
        }

    }

    // Get mot Db - søk mot databasen
    [HttpGet]
    [Route("[action]/{thought}")]
    public async Task<ActionResult<List<Thought>>> GetByThought(string thought)
    {
        List<Thought> ideas = await context.Thoughts
            .Where(idea => idea.ThoughtText != null && idea.ThoughtText.ToLower().Contains(thought.ToLower()))
            .ToListAsync();

        return ideas;
    }


    // Create
    [HttpPost]
    public async Task<ActionResult<Thought>> Post(Thought newThought)
    {
        context.Thoughts.Add(newThought);
        await context.SaveChangesAsync();
        return CreatedAtAction("GetThought", new { id = newThought.Id }, newThought);

    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Thought? thought = await context.Thoughts.FindAsync(id);

        if (thought != null)
        {
            context.Thoughts.Remove(thought);
            await context.SaveChangesAsync();
        }
        return NoContent();
    }

    // Update
    [HttpPut]
    public async Task<IActionResult> Put(Thought updateThought)
    {
        context.Entry(updateThought).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("test")]
    public IActionResult TestPost()
    {
        return Ok("POST endpoint works");
    }


}
