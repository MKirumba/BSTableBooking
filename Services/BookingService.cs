
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
    }
}


