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
       
        public DbSet<BSTableBooking.Models.BookingInfo> BookingInfo { get; set; }= default!;
        public DbSet<BSTableBooking.Models.TableArea> TableArea{ get; set; } = default!;
        public DbSet<BSTableBooking.Models.Booking> Booking { get; set; } = default!;
        public DbSet<BSTableBooking.Models.AvailTables> AvailTables { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AvailTables>()
                .HasKey(c => new { c.ProductId, c.BookDay, c.Session });

            //modelBuilder.Entity<BookingInfo>()
            //    .HasMany(a => a.ProuctId)
            //    .WithRequired(p => p. 
            //    .HasForeignKey(p => new { c.ProductId, c.BookDay, c.Session });

        }


    }

    
}

