using Microsoft.EntityFrameworkCore;
using HairBookingApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity;

namespace Moment4.Data;

public class HairContext : IdentityDbContext
{
    public HairContext(DbContextOptions<HairContext> options) : base(options)
    {
          
    }


public DbSet<BookingModel> Bookings { get; set; }
 public DbSet<CategoryModel> Categories { get; set; }
public DbSet<CustomerModel> Customers { get; set; }

    public DbSet<HairdresserModel> Hairdressers { get; set; }
        public DbSet<TimeModel> Times { get; set; }

        public DbSet<ImageModel> Images {get; set;}

}