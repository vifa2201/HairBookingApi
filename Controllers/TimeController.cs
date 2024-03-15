using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HairBookingApi.Models;
using Moment4.Data;
using Microsoft.AspNetCore.Authorization;

namespace HairBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly HairContext _context;

        public TimeController(HairContext context)
        {
            _context = context;
        }



        // GET: api/Timer
        [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeModel>>> GetTimes()
{
    // Hämta alla tider från databasen
    var allTimes = await _context.Times.ToListAsync();

    // Hämta bokade tider från databasen
    var bookedTimeIds = await _context.Bookings.Select(b => b.TimeId).ToListAsync();

    // Filtrera ut de tider som redan är bokade
    var availableTimes = allTimes.Where(t => !bookedTimeIds.Contains(t.Id)).ToList();

    return availableTimes;
}

        // GET: api/Timer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeModel>> GetTimeModel(int id)
        {
            var timeModel = await _context.Times.FindAsync(id);

            if (timeModel == null)
            {
                return NotFound();
            }

            return timeModel;
        }

        // PUT: api/Timer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutTimeModel(int id, TimeModel timeModel)
        {
            if (id != timeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeModelExists(id))
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

        // POST: api/Timer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

  [HttpPost, Authorize]
public async Task<ActionResult<TimeModel>> AddTime(TimeModel timeModel)
{
    try
    {
        // Kontrollera om tiden redan finns i databasen
        var existingTime = await _context.Times.FirstOrDefaultAsync(t => t.Time == timeModel.Time && t.Date == timeModel.Date);
        if (existingTime != null)
        {
            return Conflict("Tiden finns redan i databasen.");
        }

        // Lägg till den nya tiden
        _context.Times.Add(timeModel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTimeModel), new { id = timeModel.Id }, timeModel);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ett fel uppstod vid försöket att lägga till ny tid: " + ex.Message);
    }
}

        // DELETE: api/Timer/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteTimeModel(int id)
        {
            var timeModel = await _context.Times.FindAsync(id);
            if (timeModel == null)
            {
                return NotFound();
            }

            _context.Times.Remove(timeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimeModelExists(int id)
        {
            return _context.Times.Any(e => e.Id == id);
        }
    }
}
