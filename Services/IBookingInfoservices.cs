using BSTableBooking.Models;
using System.Security.Cryptography;

namespace BSTableBooking.Services
{
    public interface IBookingInfoservices
    {
        public IEnumerable<BookingInfo> GetAllProducts();
        public void CreateProduct(BookingInfo P);
        
    }
}
