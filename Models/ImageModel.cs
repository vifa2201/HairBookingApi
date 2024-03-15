using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairBookingApi.Models;

public class ImageModel {
    public int Id { get; set;}

      public string? ImageName { get; set; } //lagra i databasen

    [NotMapped]
    public IFormFile? ImageFile { get; set; }  //använda i formulär


}