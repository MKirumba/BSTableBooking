using BSTableBooking.Data;
using BSTableBooking.Models;
namespace BSTableBooking.Services
{
    public class BookedTableService : IBookedTableService

    {
        BSTableBookingAppDbContext Dbx;
        public BookedTableService(BSTableBookingAppDbContext _Dbx)
        {
            this.Dbx = _Dbx;
        }
        public bool Add(BookedTable model)
        {
            try
            {
                Dbx.Category.Add(model);
                Dbx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                Dbx.Category.Remove(data);
                Dbx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BookedTable GetById(int id)
        {
            return Dbx.Category.Find(id);
        }

        public IQueryable<BookedTable> List()
        {
            var data = Dbx.Category.AsQueryable();
            return data;
        }

        public bool Update(BookedTable model)
        {
            try
            {
                Dbx.Category.Update(model);
                Dbx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}
