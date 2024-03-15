using System.ComponentModel.DataAnnotations;

namespace HairBookingApi.Models;


public class CategoryModel {
    public int Id { get; set; }

     [Required(ErrorMessage = "Ange ett namn för kategorin")]
    public string? Name {get; set;}

      [Required]
     
    [Range(1, int.MaxValue, ErrorMessage = "Priset måste vara minst 1")]
    public int Price {get; set;}

    public List<BookingModel>? Bookings {get; set;}
}