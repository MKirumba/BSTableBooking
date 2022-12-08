using Microsoft.AspNetCore.Mvc;
using BSTableBooking.Services;
using BSTableBooking.Data;
using BSTableBooking.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BSTableBooking.Controllers
{
    public class BookingController : Controller
    {
        BSTableBookingAppDbContext DB;
        IBookingInfoServices IPservices;
        ITableAreaService ICService;
        IBookingService IBservice;

        public BookingController( BSTableBookingAppDbContext _Db, ITableAreaService _Categoryservices, IBookingInfoServices _IPservices, IBookingService _IBservice)
        {
            ICService = _Categoryservices;
            IPservices = _IPservices;
            DB = _Db;
            IBservice = _IBservice;

        }
        public IActionResult BookingList() 
        {
            return View(IBservice.GetAllBookings());
        }
        public IActionResult CreateOrder(int id, DateTime bookday, string sessiontime, int qty)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var newstockQty = new AvailTables()
            {
                ProductId= id,
                BookDay = bookday,
                Session = sessiontime,
                Qty = qty,
                LastUpdatedDate = DateTime.Now,

            };

            var sessionFormDb = DB.BookingInfo.Find(id);
            var stockQty = DB.AvailTables.Find(id,newstockQty.BookDay, newstockQty.Session);
            var tablelocation = DB.TableArea.Find(sessionFormDb.CategoryID);

            //sessionFormDb.Qty = stockQty.Qty;

          

            ////Session stock not found
            //if (stockQty == null)
            //{
            //    sessionFormDb.Qty = newstockQty.Qty;
               
            //}

            //sessionFormDb.Qty = stockQty.Qty;
            ////Create new partililar session


            //DB.AvailTables.Add(newstockQty);
            //DB.SaveChanges();


            // var newAvailTable = DB.AvailTables.Find(newsession.ProductId, newsession.BookDay, newsession.Session);



            //if (sessionFormDb == null)
            //{
            //    return NotFound();
            //}

            ViewData["SessionInfo"] = sessionFormDb;
            ViewData["TableLocation"] = tablelocation;


            //if (stockQty == null)
            //{
            //    return RedirectToAction("ProductList", "BookingInfo");
            //}
            //else
            //{

            //    sessionFormDb.Qty = stockQty.Qty;
            //};

            return View();
            //ViewData["Header"] = "Movie Details"
            //return View(sessionFormDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(Booking Ord)
        {

            var sessionFormDb = DB.BookingInfo.Find(Ord.ProductID);
            var Availtable = DB.AvailTables.Find(sessionFormDb.CategoryID, sessionFormDb.SessionEndTime, sessionFormDb.BookingSession);

            if (Availtable != null)
            {
                Availtable.Qty = Availtable.Qty - Ord.Qty;
                DB.AvailTables.Update(Availtable);
                DB.SaveChanges();

            }


            //if (Availtable == null)

            //{

            //    var newsession = new AvailTables()
            //    {
            //        ProductId = sessionFormDb.ProuctId,
            //        BookDay = sessionFormDb.SessionEndTime,
            //        Session = sessionFormDb.BookingSession,
            //        Qty = sessionFormDb.Qty,
            //    };

            //    DB.AvailTables.Add(newsession);
            //    DB.SaveChanges();
            //}

            
            //if (Availtable.Qty < 0)
            //{
            //    TempData["success"] = "No available seat for session. Please call to be on waiting list";
            //    return RedirectToAction("ProductList", "BookingInfo");
            //}

            DB.Booking.Add(Ord);
            DB.SaveChanges();

            //if (Availtable != null)
            //{
            //    DB.AvailTables.Update(Availtable);
            //    DB.SaveChanges();
            //}
            TempData["success"] = "Booking Submitted Successfully, your order Number is " + Ord.OrderID+" Booking details have been sent to your email";
            return RedirectToAction("Index");

        }

        // display Edit view
        [Authorize(Roles = "admin,staff")]    
        public IActionResult BookingEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFormDb = DB.Booking.Find(id);


            if (CategoryFormDb == null)
            {
                return NotFound();
            }
          

            return View(CategoryFormDb);
        }

        [Authorize(Roles = "admin,staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Booking Pobj)
        {
            //Pobj.CategoryID = Pobj.Categories;
            //var Tablearea = DB.TableArea.Find(Pobj.CategoryID);
            //Pobj.TableLocation = Tablearea.CategoryName;

            //var availcap = new AvailTables
            //{
            //    ProductId = Pobj.ProuctId,
            //    Qty = Pobj.Qty,
            //};



            //if (ModelState.IsValid)
            {
                DB.Booking.Update(Pobj);
                //DB.AvailTables.Update(availcap);
                DB.SaveChanges();



                TempData["success"] = "Session Updated Successfully";
                return RedirectToAction("BookingList");
            }
            //return View(Pobj);
        }









        public IActionResult BookingDetails(int? id)

        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var BookingFormDb = DB.Booking.Find(id);
            if (BookingFormDb == null)
            {
                return NotFound();
            }
            return View(BookingFormDb);


        }








        [Authorize(Roles = "admin")]
        public IActionResult BookingDelete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var BookingFormDb = DB.Booking.Find(id);
            if (BookingFormDb == null)
            {
                return NotFound();
            }
            return View(BookingFormDb);
        }

        // Delete Booking
        //post
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("BookingDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {

            Booking booking = DB.Booking.FirstOrDefault(s => s.OrderID == Id);
            if (booking != null)
            {
                DB.Remove(booking);
                DB.SaveChanges();
                TempData["success"] = "Booking Deleted Successfully";
                return RedirectToAction("BookingList");
            }
            return View();
        }






    }
}
