using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairBookingApi.Models;

public class HairdresserModel {
    public int Id { get; set;}

    [Required]
    public string? Name {get; set;}
    [Required]
    public string? Description {get; set;}
    [Required]
    public string? Speciality {get; set; }

      public string? ImageName { get; set; } //lagra i databasen

    [NotMapped]
    public IFormFile? ImageFile { get; set; }  //använda i formulär

       public List<BookingModel>? Bookings {get; set;}
}