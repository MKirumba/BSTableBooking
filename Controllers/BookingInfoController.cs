using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BSTableBooking.Data;
using BSTableBooking.Models;
using BSTableBooking.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BSTableBooking.Controllers
{
   
    public class BookingInfoController : Controller
    {
        BSTableBookingAppDbContext DB;
        IBookingInfoServices IPservices;
        ITableAreaService ICService;

        public BookingInfoController( BSTableBookingAppDbContext _Db, ITableAreaService _Categoryservices, IBookingInfoServices _IPservices)
        {
            ICService= _Categoryservices;
            IPservices = _IPservices;
            DB = _Db;

        }

        
        public IActionResult Index()
        {

          return View(IPservices.GetAllProducts());
        }

        public IActionResult ProductList()
        {

            return View(IPservices.GetAllProducts());
        }

        //public IActionResult Booking(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var CategoryFormDb = DB.BookingInfo.Find(id);
        //    if (CategoryFormDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CategoryFormDb);
        //}

        // display create view
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            var model = new BookingInfo();
            model.CategoryList = ICService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.CategoryId.ToString() });
            return View(model);
          
          
        }

        // save new BookingInfo
        //post
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookingInfo Pobj)
        {

            Pobj.CategoryID = Pobj.Categories;
            var Tablearea = DB.TableArea.Find(Pobj.CategoryID);
            Pobj.TableLocation = Tablearea.CategoryName;

            IPservices.CreateProduct(Pobj);
            var stock = new AvailTables
            {
                ProductId = Pobj.ProuctId,
                Session = Pobj.BookingSession,
                BookDay = Pobj.SessionEndTime,
                Qty = 0
            };  
            stock.Qty = Pobj.Qty;

 


            DB.AvailTables.Add(stock);
            DB.SaveChanges();
            TempData["success"] = "Session Created Successfully";
            return RedirectToAction("Index");
            

        }

        // display Edit view
       [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            
            var CategoryFormDb = DB.BookingInfo.Find(id);
            var Tablearea = DB.TableArea.Find(CategoryFormDb.CategoryID);
            CategoryFormDb.TableLocation = Tablearea.CategoryName;

            if (CategoryFormDb==null)
            {
                return NotFound();
            }
           CategoryFormDb.CategoryList = ICService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.CategoryId.ToString() });
            
            return View(CategoryFormDb);
        }

        // Update BookingInfo
        //post
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(BookingInfo Pobj)
        {
            Pobj.CategoryID = Pobj.Categories;
            var Tablearea = DB.TableArea.Find(Pobj.CategoryID);
            Pobj.TableLocation = Tablearea.CategoryName;

            var availcap = new AvailTables
            {
                ProductId = Pobj.ProuctId,
                Qty = Pobj.Qty,
            };



            //if (ModelState.IsValid)
            {
                DB.BookingInfo.Update(Pobj);
                DB.AvailTables.Update(availcap);
                DB.SaveChanges();



                TempData["success"] = "Session Updated Successfully";
                return RedirectToAction("Index");
            }
            //return View(Pobj);
        }

        // display Delete view

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFormDb = DB.BookingInfo.Find(id);
            if (CategoryFormDb == null)
            {
                return NotFound();
            }
            return View(CategoryFormDb);
        }

        // Delete BookingInfo
        //post
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {

            BookingInfo product = DB.BookingInfo.FirstOrDefault(s => s.ProuctId == Id);
            if (product !=null)
            {
                DB.Remove(product);
                DB.SaveChanges();
                TempData["success"] = "Booking Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        
    }
}
