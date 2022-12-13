using BSTableBooking.Models;
using System.Security.Cryptography;

namespace BSTableBooking.Services
{
    public interface ISessionServices
    {
        public IEnumerable<Session> GetAllSessions();
        public void CreateSession(Session P);
        
    }
}
