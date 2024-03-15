using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HairBookingApi.Models
{
    public class BookingModel
    {
        public int Id { get; set; }

       
  public DateTime BookingDateTime { get; set; } = DateTime.Now;

        [Required]
        public string? Message { get; set; }

        [Required]
        public bool Status { get; set; } = false;

        [Required]
        public string? HairType { get; set; }

        [Required(ErrorMessage = "Ange din hårlängd")]
        public string? HairLength { get; set; }

        public int TimeId { get; set; } // Referens till TimeModel

        public TimeModel? Time { get; set; }
           [Required(ErrorMessage = "Välj en frissör")]
        public int HairdresserId { get; set; }

        public HairdresserModel? Hairdresser { get; set; }
           [Required(ErrorMessage = "Välj en kategori")]
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel? Customer { get; set; }
    }
}