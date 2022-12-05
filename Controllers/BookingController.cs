using Microsoft.AspNetCore.Mvc;
using BSTableBooking.Services;
using BSTableBooking.Data;
using BSTableBooking.Models;

namespace BSTableBooking.Controllers
{
    public class BookingController : Controller
    {
        BSTableBookingAppDbContext DB;
        IBookingInfoservices IPservices;
        IFileService IFService;
        ITableAreaService ICService;

        public BookingController(IFileService _IFService, BSTableBookingAppDbContext _Db, ITableAreaService _Categoryservices, IBookingInfoservices _IPservices)
        {
            ICService = _Categoryservices;
            IPservices = _IPservices;
            DB = _Db;
            IFService = _IFService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateOrder(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var productFormDb = DB.BookingInfo.Find(id);
            var stockQty = DB.AvailTables.Find(id);
            productFormDb.Qty = stockQty.Qty;

            if (productFormDb == null)
            {
                return NotFound();
            }

            ViewData["BookingInfo"] = productFormDb;
            return View();
            //ViewData["Header"] = "Movie Details"
            //return View(productFormDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(Booking Ord)
        {
            var prod = DB.BookingInfo.Find(Ord.ProductID);
            Ord.TotalPrice = Ord.Qty * prod.UnitPrice;
        

            var pStock = DB.AvailTables.Find(Ord.ProductID);
            pStock.Qty = pStock.Qty - Ord.Qty;

            //var stock = new AvailTables
            //{

            //    Qty = pStock.Qty- Ord.Qty
            //};

            DB.Booking.Add(Ord);
            DB.SaveChanges();
            DB.AvailTables.Update(pStock);
            DB.SaveChanges();
            TempData["success"] = "Booking Submitted Successfully, your order Number is " + Ord.OrderID+" Booking details have been sent to your email";
            return RedirectToAction("ProductList","BookingInfo");

        }


    }
}
