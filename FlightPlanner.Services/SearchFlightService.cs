using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;

namespace FlightPlanner.Services
{
    public class SearchFlightService : EntityService<SearchFlightsRequest>, ISearchFlightService
    {
        public SearchFlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Flight> SearchFlights(SearchFlightsRequest flightRequest)
        {
            var flights = new List<Flight>();
            
            if (flightRequest.From == null || flightRequest.To == null || flightRequest.DepartureDate == null)
                return null;

            if (flightRequest.From == flightRequest.To)
                return null;

            flights = (from f in _context.Flights
                where (f.From.AirportCode == flightRequest.From && f.To.AirportCode == flightRequest.To &&
                       f.DepartureTime.Substring(0, 10) == flightRequest.DepartureDate)
                select f).ToList();

            return flights;
        }
    }
}
