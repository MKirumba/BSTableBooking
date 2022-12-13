
using BSTableBooking.Models;
using BSTableBooking.Data;

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


