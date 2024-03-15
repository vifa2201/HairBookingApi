using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace HairBookingApi.Models;


public class CustomerModel
{
    public int Id { get; set; }


    public string? Name { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }


    public List<BookingModel>? Bookings {get; set;}

}