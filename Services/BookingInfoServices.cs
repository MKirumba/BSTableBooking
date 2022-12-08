using BSTableBooking.Models;
using BSTableBooking.Data;

namespace BSTableBooking.Services
{
    public class BookingInfoService: IBookingInfoServices
    {
        
        BSTableBookingAppDbContext Db;
        public BookingInfoService(BSTableBookingAppDbContext _Db)
        {
            Db = _Db;
        }
        public IEnumerable<BookingInfo> GetAllProducts()
        {
            return (Db.BookingInfo.Select(u => u).ToList());
        }
        public void CreateProduct(BookingInfo P)
        {
          
                Db.BookingInfo.Add(P);
                Db.SaveChanges();
        }
    }
}
