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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace BSTableBooking.Controllers
{
    public class BookingController : Controller
    {
        BSTableBookingAppDbContext DB;
        ISessionServices IPservices;
        ITableAreaService ICService;
        IBookingService IBservice;

        public BookingController( BSTableBookingAppDbContext _Db, ITableAreaService _TableAreaservices, ISessionServices _IPservices, IBookingService _IBservice)
        {
            ICService = _TableAreaservices;
            IPservices = _IPservices;
            DB = _Db;
            IBservice = _IBservice;

        }
        public IActionResult BookingList() 
        {
            return View(IBservice.GetAllBookings());
        }
        public IActionResult CreateBooking(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var sessionFormDb = DB.Session.Find(id);
            var FreeTablesQty = DB.AvailTables.Find(id);
            var tablearealocation = DB.TableArea.Find(sessionFormDb.TableAreaID);

    

            ViewData["SessionInfo"] = sessionFormDb;
            ViewData["TableLocation"] = tablearealocation;
            ViewData["available"] = FreeTablesQty;

      

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking Pobj)
        {

            // Getting Session quantity info
            var sessionexists = DB.AvailTables.Find(Pobj.SessionID);
            Pobj.SessionID = sessionexists.SessionID;
            if (sessionexists != null)
            {

                sessionexists.Qty = sessionexists.Qty - Pobj.Qty;

                if (sessionexists.Qty < 0)
                {

                    TempData["noseats"] = "No available seat for session. Please call to be on waiting list";
                    return RedirectToAction("SessionList", "Session");


                }
                // Updating Session quantity
                DB.AvailTables.Update(sessionexists);
                DB.SaveChanges();
            }

            /// Create Booking in database
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    IBservice.CreateBooking(Pobj);

                    transaction.Commit();

                    TempData["success"] = "Booking Submitted Successfully, your Booking Number is " + Pobj.BookingID
                        + " Booking details have been sent to your email";

                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }

            }
  
            return RedirectToAction("BookingList");

        }

        // display Edit view
        [Authorize(Roles = "admin,staff")]
        public IActionResult BookingEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var booking = DB.Booking.Find(id);


            if (booking == null)
            {
                return NotFound();
            }
            
            var sessionFormDb = DB.Session.Find(booking.SessionID);
            var FreeTablesQty = DB.AvailTables.Find(booking.SessionID);
    
            ViewData["SessionInfo"] = sessionFormDb;
            ViewData["available"] = FreeTablesQty;


            return View(booking);
        }

        [Authorize(Roles = "admin,staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult BookingEdit(Booking Pobj)
        {
            var booking = DB.Booking.Find(Pobj.BookingID);
            Pobj.SessionID = booking.SessionID;

            var sessionexists = DB.AvailTables.Find(Pobj.SessionID);
            Pobj.SessionID = sessionexists.SessionID;

            /// Check for session information///

            if (Pobj.Qty < sessionexists.Qty)
            {
                sessionexists.Qty = sessionexists.Qty - Pobj.Qty;

                    if (sessionexists.Qty < 0)
                    {

                        TempData["noseats"] = "Not enough seats for change to session. Please call to be on waiting list";
                        return RedirectToAction("SessionList", "Session");

                    }

                DB.AvailTables.Update(sessionexists);
                DB.SaveChanges();
                }


            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    DB.Booking.Remove(booking);
                    IBservice.UpdateBooking(Pobj);

                    transaction.Commit();


                    TempData["success"] = "Booking Updated Successfully, your new booking number is " + Pobj.BookingID
                        + " Booking details have been sent to your email";

                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }

            }

            return RedirectToAction("BookingList");

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

            Booking booking = DB.Booking 
                .FirstOrDefault(s => s.BookingID == Id);
            if (booking != null)
            {
                DB.Remove(booking);
                DB.SaveChanges();
                TempData["success"] = "Booking Deleted Successfully";
                return RedirectToAction("BookingList");
            }
            return View();
        }

        // checks of booking exists
        private bool BookingExists(int id)
        {
            return DB.Booking.Any(e => e.BookingID == id);
        }




    }
}
