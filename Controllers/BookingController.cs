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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HairBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly HairContext _context;

        public BookingController(HairContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingModel>>> GetBookings()
        {
            var bookings = _context.Bookings.Include(a=> a.Time).ToList();
            return Ok(bookings);
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
     public async Task<ActionResult<BookingModel>> GetBookingModel(int id)
{
     var options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

    //inkluderar mer info
    var bookingModel = await _context.Bookings
        .Include(booking => booking.Customer)
        .Include(booking => booking.Category)
        .Include(booking => booking.Hairdresser)
           .Include(booking => booking.Time)
        .FirstOrDefaultAsync(booking => booking.Id == id);

    if (bookingModel == null)
    {
        return NotFound();
    }

     return Ok(JsonSerializer.Serialize(bookingModel, options));
}

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingModel(int id, BookingModel bookingModel)
        {
            if (id != bookingModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookingModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingModelExists(id))
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

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<BookingModel>> PostBookingModel(BookingModel bookingModel)
        {
            // Kontrollera om tiden är tillgänglig för bokning
            var time = await _context.Times.FindAsync(bookingModel.TimeId);
                    if (time == null)
        {
            return NotFound("Tiden kunde inte hittas.");
        }

            if (time == null || !time.Available)
            {
                return BadRequest("Den valda tiden är inte tillgänglig för bokning.");
            }

            // Markera tiden som upptagen
            time.Available = false;
            _context.Entry(time).State = EntityState.Modified;

            // Lägg till bokningen
            _context.Bookings.Add(bookingModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingModel", new { id = bookingModel.Id }, bookingModel);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingModel(int id)
        {
            var bookingModel = await _context.Bookings.FindAsync(id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(bookingModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingModelExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
