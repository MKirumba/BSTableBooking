using BSTableBooking.Data;
using BSTableBooking.Models;
using BSTableBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BSTableBooking.Controllers
{

    public class SessionController : Controller
    {
        BSTableBookingAppDbContext DB;
        ISessionServices IPservices;
        ITableAreaService ICService;
        IFileService IFservice;


        public SessionController(BSTableBookingAppDbContext _Db, ITableAreaService _TableAreaservices, ISessionServices _IPservices, IFileService _IFservice)
        {
            ICService = _TableAreaservices;
            IPservices = _IPservices;
            DB = _Db;
            IFservice = _IFservice;
        }

        [Authorize]
        public IActionResult Index()
        {

            return View(IPservices.GetAllSessions());
        }
        [Authorize]
        public IActionResult SessionList()
        {
            return View(IPservices.GetAllSessions());
        }


        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var model = new Session();

            model.TableAreaList = ICService.List().Select(a => new SelectListItem { Text = a.TableAreaName, Value = a.TableAreaID.ToString() });




            return View(model);


        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Session Pobj)
        {

            var Tablearea = DB.TableArea.Find(Pobj.TableAreaID);


            if (Tablearea == null)
            {
                var tableloc = new TableArea
                {

                    TableAreaName = Pobj.TableLocation,
                    TableAreaDescription = Pobj.TableLocation
                };
                DB.TableArea.Add(tableloc);
                DB.SaveChanges();

                Pobj.TableAreaID = tableloc.TableAreaID;

            }

            // Add Image
            Pobj.Image = Pobj.ImageFile.FileName;
            if (Pobj.ImageFile != null)
            {
                var fileReult = this.IFservice.SaveImage(Pobj.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(Pobj);
                }
                var imageName = fileReult.Item2;
                Pobj.Image = imageName;
            }

            // Update Database
            using (var transaction = DB.Database.BeginTransaction())
            {
                try
                {

                    IPservices.CreateSession(Pobj);

                    transaction.Commit();
                    TempData["success"] = "Session Created Successfully";
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }

                var availtab = new AvailTables
                {
                    SessionID = Pobj.SessionID,

                    Qty = Pobj.Qty,
                };
                DB.AvailTables.Add(availtab);
                DB.SaveChanges();

            }


            return RedirectToAction("Index");

        }


        // display Edit view
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var TableAreaFormDb = DB.Session.Find(id);
            //   var Tablearea = DB.TableArea.Find(TableAreaFormDb.TableAreaID);
            //  TableAreaFormDb.TableLocation = Tablearea.TableAreaName;

            if (TableAreaFormDb == null)
            {
                return NotFound();
            }


            //   TableAreaFormDb.TableAreaList = ICService.List()
            //     .Select(a => new SelectListItem { Text = a.TableAreaName, Value = a.TableAreaID.ToString() });

            return View(TableAreaFormDb);
        }

        // Update Session
        //post
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Session Pobj)
        {

            DB.Session.Update(Pobj);
            DB.SaveChanges();

            var tableloc = new TableArea
            {

                TableAreaName = Pobj.TableLocation,
                TableAreaDescription = Pobj.TableLocation
            };

            DB.TableArea.Update(tableloc);

            TempData["success"] = "Session Updated Successfully";


            var availtable = DB.AvailTables.Find(Pobj.SessionID);

            availtable.SessionID = Pobj.SessionID;
            availtable.Qty = Pobj.Qty;
            DB.AvailTables.Update(availtable);
            DB.SaveChanges();

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var TableAreaFormDb = DB.Session.Find(id);
            if (TableAreaFormDb == null)
            {
                return NotFound();
            }
            return View(TableAreaFormDb);
        }

        // Delete Session
        //post
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {

            Session Session = DB.Session.FirstOrDefault(s => s.SessionID == Id);
            if (Session != null)
            {
                DB.Remove(Session);
                DB.SaveChanges();
                TempData["success"] = "Booking Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

    }
}

