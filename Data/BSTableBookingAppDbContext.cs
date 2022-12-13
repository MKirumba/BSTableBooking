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
       
        public DbSet<BSTableBooking.Models.Session> Session { get; set; }= default!;
        public DbSet<BSTableBooking.Models.TableArea> TableArea{ get; set; } = default!;
        public DbSet<BSTableBooking.Models.Booking> Booking { get; set; } = default!;
        public DbSet<BSTableBooking.Models.AvailTables> AvailTables { get; set; } = default!;
         

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    //modelBuilder.Entity<AvailTables>()
        //    //    .HasKey(c => new { c.SessionId, c.BookDay, c.SessionSlot });

        //    //modelBuilder.Entity<Session>()
        //    //    .HasMany(a => a.SessionID)
        //    //    .WithRequired(p => p. 
        //    //    .HasForeignKey(p => new { c.SessionId, c.BookDay, c.Session });

        //}


    }

    
}

