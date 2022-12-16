using BSTableBooking.Data;
using BSTableBooking.Models;

namespace BSTableBooking.Services
{
    public class SessionService : ISessionServices
    {

        BSTableBookingAppDbContext Db;
        public SessionService(BSTableBookingAppDbContext _Db)
        {
            Db = _Db;
        }
        public IEnumerable<Session> GetAllSessions()
        {
            return (Db.Session.Select(u => u).ToList());
        }
        public void CreateSession(Session P)
        {

            Db.Session.Add(P);
            Db.SaveChanges();
        }
    }
}
