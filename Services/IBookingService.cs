﻿using BSTableBooking.Models;
using System.Security.Cryptography;

namespace BSTableBooking.Services
{
    public interface IBookingService
    {
        public IEnumerable<Booking> GetAllBookings();
        public void CreateBooking(Booking P);
        public void UpdateBooking(Booking P);
        public void DeleteBooking(Booking P);

        public IQueryable<Booking> List();
    }
}
