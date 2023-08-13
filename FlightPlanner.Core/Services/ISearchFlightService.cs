using System.Collections.Generic;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface ISearchFlightService : IEntityService<SearchFlightsRequest>
    {
        public List<Flight> SearchFlights(SearchFlightsRequest flightRequest);
    }
}
