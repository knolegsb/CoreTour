using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTour.Models
{
    public interface ICoreTourRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName, string userName);

        void AddTrip(Trip trip);
        void AddStop(string tripName, string userName, Stop newStop);

        Task<bool> SaveChangesAsync();
        object GetTripsByUserName(string name);
    }
}
