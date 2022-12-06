using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BSTableBooking.Data;
using BSTableBooking.Models;
using BSTableBooking.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BSTableBooking.Controllers
{
   
    public class BookingInfoController : Controller
    {
        BSTableBookingAppDbContext DB;
        IBookingInfoservices IPservices;
        ITableAreaService ICService;

        public BookingInfoController( BSTableBookingAppDbContext _Db, ITableAreaService _Categoryservices, IBookingInfoservices _IPservices)
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
            model.CategoryList = ICService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.CategoryId.ToString()});
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
 
            IPservices.CreateProduct(Pobj);
            var stock = new AvailTables
            {
                ProductId = Pobj.ProuctId,
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
            
           
            //if (ModelState.IsValid)
            {
                DB.BookingInfo.Update(Pobj);
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
