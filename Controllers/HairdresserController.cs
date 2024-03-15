using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HairBookingApi.Models;
using Moment4.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace HairBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairdresserController : ControllerBase
    {
        private readonly HairContext _context;
        //denna måste vara static för att det ska fungera!
        public static IWebHostEnvironment _hostEnviroment;
        private readonly string wwwRootPath;


        public HairdresserController(HairContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnviroment = hostEnvironment;
            //sökväg till wwroot för bilder
            wwwRootPath = hostEnvironment.WebRootPath;
        }

        // GET: api/Hairdresser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HairdresserModel>>> GetHairdressers()
        {
            var hairdressers = await _context.Hairdressers.ToListAsync();

            // Uppdatera bildens sökväg för varje frisör
            foreach (var hairdresser in hairdressers)
            {
                hairdresser.ImageName = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/images/{hairdresser.ImageName}";
            }

            return hairdressers;
        }

        // GET: api/Hairdresser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HairdresserModel>> GetHairdresserModel(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var hairdresserModel = await _context.Hairdressers
            .Include(hairdresser => hairdresser.Bookings)
                      .ThenInclude(booking => booking.Time)
            .FirstOrDefaultAsync(hairdresser => hairdresser.Id == id);

            if (hairdresserModel == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(hairdresserModel, options));
        }

        // PUT: api/Hairdresser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPut("{id}"), Authorize]
public async Task<IActionResult> PutHairdresserModel(int id, HairdresserModel hairdresserModel)
{
    if (id != hairdresserModel.Id)
    {
        return BadRequest();
    }

    // Kolla om en ny bild har skickats från klienten
    if (hairdresserModel.ImageFile != null)
    {
        // Generera nytt filnamn
        string fileName = Path.GetFileNameWithoutExtension(hairdresserModel.ImageFile.FileName);
        string extension = Path.GetExtension(hairdresserModel.ImageFile.FileName);

        hairdresserModel.ImageName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;

        string path = Path.Combine(wwwRootPath + "/images", hairdresserModel.ImageName);

        // Spara den nya bilden i filsystemet
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await hairdresserModel.ImageFile.CopyToAsync(fileStream);
        }
    }

    _context.Entry(hairdresserModel).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!HairdresserModelExists(id))
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

    
        // POST: api/Hairdresser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       [HttpPost, Authorize]
        public async Task<ActionResult<HairdresserModel>> PostHairdresserModel(HairdresserModel hairdresserModel)
        {
            //kolla efter bild
            if(hairdresserModel.ImageFile != null)
            {
                //generera nytt filnamn
                string fileName = Path.GetFileNameWithoutExtension(hairdresserModel.ImageFile.FileName);
                string extension = Path.GetExtension(hairdresserModel.ImageFile.FileName);

                hairdresserModel.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwRootPath + "/images", fileName);

                //spara i filsystem
                using (var fileStream = new FileStream(path, FileMode.Create)){
                    await hairdresserModel.ImageFile.CopyToAsync(fileStream);
                }

            }
          
            _context.Hairdressers.Add(hairdresserModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHairdresserModel", new { id = hairdresserModel.Id }, hairdresserModel);
        }

        // DELETE: api/Hairdresser/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteHairdresserModel(int id)
        {
            var hairdresserModel = await _context.Hairdressers.FindAsync(id);
            if (hairdresserModel == null)
            {
                return NotFound();
            }

            _context.Hairdressers.Remove(hairdresserModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HairdresserModelExists(int id)
        {
            return _context.Hairdressers.Any(e => e.Id == id);
        }
    }
}
