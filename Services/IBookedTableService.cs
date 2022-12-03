using BSTableBooking.Models;

namespace BSTableBooking.Services
{
    public interface IBookedTableService
    {
        public bool Add(BookedTable model);

        public bool Update(BookedTable model);

        public BookedTable GetById(int id);

        public bool Delete(int id);

        public IQueryable<BookedTable> List();
    }
}
