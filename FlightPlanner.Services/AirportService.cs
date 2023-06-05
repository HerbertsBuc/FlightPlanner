using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<Airport> SearchAirports(string airport)
        {
            List<Airport> airports = new List<Airport>();

            if (string.IsNullOrEmpty(airport))
            {
                return null;
            }

            foreach (Flight f in _context.Flights)
            {
                if (f.From.Country.ToLower().Contains(airport.Trim().ToLower()) ||
                    f.From.City.ToLower().Contains(airport.Trim().ToLower()) ||
                    f.From.AirportCode.ToLower().Contains(airport.Trim().ToLower()))
                {
                    airports.Add(f.From);
                }

                if (f.To.Country.ToLower().Contains(airport.Trim().ToLower()) ||
                    f.To.City.ToLower().Contains(airport.Trim().ToLower()) ||
                    f.To.AirportCode.ToLower().Contains(airport.Trim().ToLower()))
                {
                    airports.Add(f.To);
                }
            }

            return airports;
        }
    }
}
