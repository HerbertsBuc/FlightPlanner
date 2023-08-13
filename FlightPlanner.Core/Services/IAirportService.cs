using System.Collections.Generic;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        public List<Airport> SearchAirports(string airport);
    }
}
