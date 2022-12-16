using BSTableBooking.Data;
using BSTableBooking.Models;
namespace BSTableBooking.Services
{
    public class TableAreaService : ITableAreaService

    {
        BSTableBookingAppDbContext Dbx;
        public TableAreaService(BSTableBookingAppDbContext _Dbx)
        {
            this.Dbx = _Dbx;
        }
        public bool Add(TableArea model)
        {
            try
            {
                Dbx.TableArea.Add(model);
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
                Dbx.TableArea.Remove(data);
                Dbx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TableArea GetById(int id)
        {
            return Dbx.TableArea.Find(id);
        }

        public IQueryable<TableArea> List()
        {
            var data = Dbx.TableArea.AsQueryable();
            return data;
        }

        public bool Update(TableArea model)
        {
            try
            {

                Dbx.TableArea.Update(model);
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
