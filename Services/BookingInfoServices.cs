using BSTableBooking.Models;
using BSTableBooking.Data;
namespace BSTableBooking.Services
{
    public class BookingInfoServices: IBookingInfoservices
    {
        
        BSTableBookingAppDbContext Db;
        public BookingInfoServices(BSTableBookingAppDbContext _Db)
        {
            Db = _Db;
        }
        public IEnumerable<BookingInfo> GetAllProducts()
        {
            return (Db.Product.Select(u => u).ToList());
        }
        public void CreateProduct(BookingInfo P)
        {
          
                Db.Product.Add(P);
                Db.SaveChanges();
        }
    }
}
