using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }
        public List<Airport> SearchAirports(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return null;
            }

            var airports = from f in _context.Airports
                           where f.Country.ToLower().Contains(phrase.Trim().ToLower()) ||
                                  f.City.ToLower().Contains(phrase.Trim().ToLower()) ||
                                  f.AirportCode.ToLower().Contains(phrase.Trim().ToLower())
                           select f;

            return airports.ToList();
        }
    }
}
