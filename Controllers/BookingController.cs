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
             
            //var sessionexists = DB.AvailTables.Find(Pobj.avSessionID.SessionID);

            //if (sessionexists != null)
            //{
             
            //    sessionexists.Qty = sessionexists.Qty -  Pobj.Qty;
            
            //    DB.AvailTables.Update(sessionexists);
            //    DB.SaveChanges();
            //}


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

            //var sessionexists = DB.AvailTables.Find(Pobj.SessionID);

            //if (sessionexists != null)
            //{
            //    var bksession = new AvailTables()
            //    {
            //        SessionID = sessionexists.SessionID,
               
            //        Qty = Pobj.Qty,

            //    };
            //    DB.AvailTables.Add(bksession);
            //    DB.SaveChanges();
            //}




            //var booking = new Booking();
            ////booking.BookingID = Pobj.BookingID;

            //var sessionexists = DB.AvailTables.Find(Pobj.BookingID);
            //var sessionFormDb = DB.Session.Find(Pobj.SessionID);


            ///// Check for session information///

            //if (sessionexists != null)
            //{


            //    sessionexists.Qty = sessionexists.Qty - Pobj.Qty;

            //    if (sessionexists.Qty < 0)
            //    {

            //        TempData["noseats"] = "No available seat for session. Please call to be on waiting list";
            //        return RedirectToAction("SessionList", "Session");


            //    }

            //    DB.AvailTables.Update(sessionexists);
            //    DB.SaveChanges();

            //}
            //if (sessionexists == null)
            //{
            //    var bksession = new AvailTables()
            //    {

            //        BookingID = Pobj.BookingID,
            //        Qty = sessionFormDb.Qty,
            //        SessionID = sessionFormDb.SessionID


            //    };
            //    DB.AvailTables.Add(bksession);
            //    DB.SaveChanges();

            //}


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

            var sessionFormDb = DB.Session.Find(id);
            var FreeTablesQty = DB.AvailTables.Find(booking.BookingID);
            var tablearealocation = DB.TableArea.Find(sessionFormDb.TableAreaID);
          

            ViewData["SessionInfo"] = sessionFormDb;
            ViewData["TableLocation"] = tablearealocation;
            ViewData["available"] = FreeTablesQty;


            if (booking == null)
            {
                return NotFound();
            }

  
          // TableAreaFormDb = IBservice.List().Select(a => new SelectListItem { Text = a. Value = a.TableArea.ToString() });

            return View(booking);
        }

        [Authorize(Roles = "admin,staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult BookingEdit(Booking Pobj)
        {
            var booking = new Booking();

            booking.BookingID = Pobj.BookingID;

            var sessionFormDb = DB.Session
               // .Find(Pobj.Session.SessionID);
                    .Find(Pobj.Session);
            var sessionexists = DB.AvailTables
                //.Find(Pobj.Session.SessionID);
                    .Find(Pobj.Session);
            /// Check for session information///

            if (sessionexists != null)
            {


                sessionexists.Qty = sessionexists.Qty - Pobj.Qty;

                if (sessionexists.Qty < 0)
                {

                    TempData["noseats"] = "No available seat for session. Please call to be on waiting list";
                    return RedirectToAction("SessionList", "Session");


                }

                DB.AvailTables.Update(sessionexists);
                DB.SaveChanges();

            }
            if (sessionexists == null)
            {
                var bksession = new AvailTables()
                {
                    //SessionID = (int)Pobj.Session.SessionID,
                    SessionID = sessionFormDb.SessionID,
                    Qty = sessionFormDb.Qty,

                };
                DB.AvailTables.Add(bksession);
                DB.SaveChanges();

            }


            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {
                    //DeletePost(Pobj.Session.SessionID);
                    IBservice.UpdateBooking(Pobj);

                    transaction.Commit();


                    TempData["success"] = "Booking Updated Successfully, your Booking Number is " + Pobj.BookingID
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
