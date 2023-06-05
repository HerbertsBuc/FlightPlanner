using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface ISearchFlightService : IEntityService<SearchFlightsRequest>
    {
        public List<Flight> SearchFlights(SearchFlightsRequest flightRequest);
    }
}
