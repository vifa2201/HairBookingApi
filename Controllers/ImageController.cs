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
    public class ImageController : ControllerBase 
    {
        private readonly HairContext _context;
         public static IWebHostEnvironment _hostEnviroment;
        private readonly string wwwRootPath;

        public ImageController(HairContext context, IWebHostEnvironment hostEnvironment)
        {
               _context = context;
            _hostEnviroment = hostEnvironment;
            //sökväg till wwroot för bilder
            wwwRootPath = hostEnvironment.WebRootPath;
        }

        // GET: api/Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageModel>>> GetImages()
        {
                      var images = await _context.Images.ToListAsync();

            // Uppdatera bildens sökväg för varje frisör
            foreach (var image in images)
            {
                image.ImageName = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/images/{image.ImageName}";
            }

            return images;
        }

        // GET: api/Image/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageModel>> GetImageModel(int id)
        {
            var imageModel = await _context.Images.FindAsync(id);

            if (imageModel == null)
            {
                return NotFound();
            }

            return imageModel;
        }

        // PUT: api/Image/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutImageModel(int id, ImageModel imageModel)
        {
            if (id != imageModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(imageModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageModelExists(id))
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

        // POST: api/Image
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<ImageModel>> PostImageModel(ImageModel imageModel)
        {
              //kolla efter bild
            if(imageModel.ImageFile != null)
            {
                                //generera nytt filnamn
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);

                    imageModel.ImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;

                      string path = Path.Combine(wwwRootPath + "/images", fileName);

                         //spara i filsystem
                using (var fileStream = new FileStream(path, FileMode.Create)){
                    await imageModel.ImageFile.CopyToAsync(fileStream);
                }
            }
            _context.Images.Add(imageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageModel", new { id = imageModel.Id }, imageModel);
        }

        // DELETE: api/Image/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteImageModel(int id)
        {
            var imageModel = await _context.Images.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }

            _context.Images.Remove(imageModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageModelExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
