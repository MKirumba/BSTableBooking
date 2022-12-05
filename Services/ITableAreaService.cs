using BSTableBooking.Models;

namespace BSTableBooking.Services
{
    public interface ITableAreaService
    {
        public bool Add(TableArea model);

        public bool Update(TableArea model);

        public TableArea GetById(int id);

        public bool Delete(int id);

        public IQueryable<TableArea> List();
    }
}
