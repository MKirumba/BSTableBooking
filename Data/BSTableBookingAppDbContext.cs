using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BSTableBooking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTableBooking.Data
{
    public class BSTableBookingAppDbContext: IdentityDbContext<ApplicationUser>
    {
        public BSTableBookingAppDbContext(DbContextOptions<BSTableBookingAppDbContext> options):base(options)
        {
            
        }
       
        public DbSet<BSTableBooking.Models.BookingInfo> Product { get; set; }= default!;
        public DbSet<BSTableBooking.Models.BookedTable> Category { get; set; } = default!;
        public DbSet<BSTableBooking.Models.Booking> Order { get; set; } = default!;
        public DbSet<BSTableBooking.Models.AvailTables> Stock { get; set; } = default!;

       
       

    }
}

