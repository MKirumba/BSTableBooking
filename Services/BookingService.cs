
using BSTableBooking.Data;
using BSTableBooking.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BSTableBooking.Services
{
    public class BookingService : IBookingService
    {

        BSTableBookingAppDbContext Db;
        public BookingService(BSTableBookingAppDbContext _Db)
        {
            Db = _Db;
        }
        public IEnumerable<Booking> GetAllBookings()
        {
            return (Db.Booking.Select(u => u).ToList());
        }

        public IEnumerable<Booking> GetAllBookings(string search)
        {

            var customer = from m in Db.Booking
                           select m;

            if (!String.IsNullOrEmpty(search))
            {
                customer = customer.Where(s => s.CustomerIdentity.Contains(search));
            }

            return (customer);
        }



        public void CreateBooking(Booking P)
        {

            Db.Booking.Add(P);
            Db.SaveChanges();
        }

        public void UpdateBooking(Booking P)
        {

            Db.Booking.Update(P);
            Db.SaveChanges();
        }

        public void DeleteBooking(Booking P)
        {

            Db.Booking.Remove(P);
            Db.SaveChanges();
        }




        public IQueryable<Booking> List()
        {
            var data = Db.Booking.AsQueryable();
            return data;
        }

    }
}


