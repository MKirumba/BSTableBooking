using BSTableBooking.Models;

namespace BSTableBooking.Services
{
    public interface ISessionServices
    {
        public IEnumerable<Session> GetAllSessions();
        public void CreateSession(Session P);

    }
}
