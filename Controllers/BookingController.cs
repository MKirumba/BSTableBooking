using BSTableBooking.Data;
using BSTableBooking.Models;
using BSTableBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace BSTableBooking.Controllers
{
    public class BookingController : Controller
    {
        /// <summary>
        /// Data Base instance for use by controller
        /// Interace instance for use by controller
        /// </summary>
        BSTableBookingAppDbContext DB;
        ISessionServices IPservices;
        ITableAreaService ICService;
        IBookingService IBservice;

        public BookingController(BSTableBookingAppDbContext _Db, ITableAreaService _TableAreaservices, ISessionServices _IPservices, IBookingService _IBservice)
        {
            ICService = _TableAreaservices;
            IPservices = _IPservices;
            DB = _Db;
            IBservice = _IBservice;

        }

        /// <summary>
        /// Controller for View showing List of Bookings made
        /// </summary>
        /// <returns></returns>

        public IActionResult BookingList()
        {
            return View(IBservice.GetAllBookings());
        }

        /// <summary>
        /// Controller for Creating Bookings
        /// SessionID passed to make booking too
        /// </summary>
        /// <param name="id">Session ID</param>
        /// <returns></returns>

        public IActionResult CreateBooking(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve available database information about table and session
            var sessionFormDb = DB.Session.Find(id);
            var FreeTablesQty = DB.AvailTables.Find(id);
            var tablearealocation = DB.TableArea.Find(sessionFormDb.TableAreaID);


            // Make information available to View
            ViewData["SessionInfo"] = sessionFormDb;
            ViewData["TableLocation"] = tablearealocation;
            ViewData["available"] = FreeTablesQty;



            return View();

        }
        /// <summary>
        ///  Create booking by post
        /// </summary>
        /// <param name="Pobj">Booking Object</param>
        /// <returns></returns>
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
            //var currentuser = User.Identity.Name;

          
            return RedirectToAction("SessionList", "Session");

        }

        /// <summary>
        /// Display View for Edit
        /// </summary>
        /// <param name="id">BookingID</param>
        /// <returns></returns>
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
            /// Get session info from Database 
            var sessionFormDb = DB.Session.Find(booking.SessionID);
            var FreeTablesQty = DB.AvailTables.Find(booking.SessionID);

            /// Send info to view (read only)
            ViewData["SessionInfo"] = sessionFormDb;
            ViewData["available"] = FreeTablesQty;


            return View(booking);
        }


        /// <summary>
        /// Update Booking and post
        /// </summary>
        /// <param name="Pobj">Booking Object</param>
        /// <returns></returns>

        [Authorize(Roles = "admin,staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult BookingEdit(Booking Pobj)
        {
            // Get booking information from database
            var booking = DB.Booking.Find(Pobj.BookingID);
            Pobj.SessionID = booking.SessionID;


            /// Get session information
            var sessionexists = DB.AvailTables.Find(Pobj.SessionID);
            Pobj.SessionID = sessionexists.SessionID;

            /// Check for session information///
            
            //check change in the number of guest
            int peoplechange = Pobj.Qty - booking.Qty;


            if (Pobj.Qty == booking.Qty)
            {
                sessionexists.Qty = Pobj.Qty;

            }       
            if (sessionexists.Qty < Pobj.Qty)
            {

                TempData["noseats"] = "Not enough seats for change to session. Please call to be on waiting list";
                return RedirectToAction("SessionList", "Session");
            }

            //change to available tables 
            if (peoplechange  != 0)
            {
                sessionexists.Qty = sessionexists.Qty - peoplechange; 
                

            }

                // Update session available tables if it needs updating
                DB.AvailTables.Update(sessionexists);
                DB.SaveChanges();
            

            // Execute transaction (if not there is a graceful fallback)
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

        /// <summary>
        /// Deletes booking
        /// </summary>
        /// <param name="id">BookingId</param>
        /// <returns></returns>
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

        /// <summary>
        /// post for Dekete
        /// </summary>
        /// <param name="Id">Booking ID</param>
        /// <returns></returns>

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
