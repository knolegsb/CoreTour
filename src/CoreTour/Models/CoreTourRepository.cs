using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTour.Models
{
    public class CoreTourRepository : ICoreTourRepository
    {
        private CoreTourDbContext _context;
        private ILogger<CoreTourRepository> _logger;

        public CoreTourRepository(CoreTourDbContext context, ILogger<CoreTourRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, string userName, Stop newStop)
        {
            var trip = GetTripByName(tripName, userName);

            if (trip != null)
            {
                trip.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from the Database");
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName && t.UserName == userName)
                .FirstOrDefault();
        }

        public object GetTripsByUsername(string name)
        {
            return _context.Trips
                .Where(t => t.UserName == name)
                .ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
