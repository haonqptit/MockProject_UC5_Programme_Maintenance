using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockProject.Data;
using MockProject.Models;

namespace MockProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgrammesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProgrammesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Programme>>> GetProgrammes(bool? isActive)
    {
        if (isActive.HasValue)
        {
            return await _context.Programmes.Where(p => p.IsActive == isActive.Value).ToListAsync();
        }

        return await _context.Programmes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Programme>> GetProgramme(int id)
    {
        var programme = await _context.Programmes.FindAsync(id);

        if (programme == null)
        {
            return NotFound();
        }

        return programme;
    }

    [HttpPost]
    public async Task<ActionResult<Programme>> CreateProgramme(Programme programme)
    {
        if (await _context.Programmes.AnyAsync(p => p.Name == programme.Name))
        {
            return BadRequest("Programme name must be unique.");
        }

        var contact = await _context.Contacts.FindAsync(programme.ContactId);
        if (contact == null)
        {
            return BadRequest("Invalid ContactId.");
        }

        _context.Programmes.Add(programme);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProgramme), new { id = programme.Id }, programme);
    }
    
    

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgramme(int id, Programme programme)
    {
        if (id != programme.Id)
        {
            return BadRequest();
        }

        _context.Entry(programme).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProgrammeExists(id))
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

    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> DeactivateProgramme(int id)
    {
        var programme = await _context.Programmes.FindAsync(id);
        if (programme == null)
        {
            return NotFound();
        }

        programme.IsActive = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProgrammeExists(int id)
    {
        return _context.Programmes.Any(e => e.Id == id);
    }
}