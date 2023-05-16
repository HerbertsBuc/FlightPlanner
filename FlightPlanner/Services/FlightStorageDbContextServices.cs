using System;
using System.Collections.Generic;
using FlightPlanner.Models;


namespace FlightPlanner.Services
{
    public class FlightStorageDbContextServices
    {

        private protected FlightPlannerDbContext _context;
        public FlightStorageDbContextServices(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public bool FlightHasNullValues(Flight flight)
        {
            if (flight.From == null || flight.To == null ||
                string.IsNullOrEmpty(flight.From.AirportCode) ||
                string.IsNullOrEmpty(flight.From.City) ||
                string.IsNullOrEmpty(flight.From.Country) ||
                string.IsNullOrEmpty(flight.To.AirportCode) ||
                string.IsNullOrEmpty(flight.To.City) ||
                string.IsNullOrEmpty(flight.To.Country) ||
                string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.DepartureTime) ||
                string.IsNullOrEmpty(flight.ArrivalTime))
            {
                return true;
            }

            return false;
        }
        public bool SameAirport(Flight flight)
        {
            if (flight.From == flight.To ||
                flight.From.City.ToLower() == flight.To.City.ToLower() ||
                flight.From.Country.ToLower() == flight.To.Country.ToLower() ||
                flight.From.AirportCode.ToLower() == flight.To.AirportCode.ToLower())
            {
                return true;
            }

            return false;
        }

        public bool ArrivalBeforeDeparture(Flight flight)
        {

            DateTime departure = DateTime.Parse(flight.DepartureTime);
            DateTime arrival = DateTime.Parse(flight.ArrivalTime);

            if (departure >= arrival)
            {
                return true;
            }

            return false;

        }

        public bool FlightExists(Flight flight)
        {
            foreach (Flight f in _context.Flights)
            {
                if (f.From.AirportCode == flight.From.AirportCode &&
                    f.From.City == flight.From.City &&
                    f.From.Country == flight.From.Country &&
                    f.To.AirportCode == flight.To.AirportCode &&
                    f.To.City == flight.To.City &&
                    f.To.Country == flight.To.Country &&
                    f.Carrier == flight.Carrier &&
                    f.DepartureTime == flight.DepartureTime &&
                    f.ArrivalTime == flight.ArrivalTime)
                {
                    return true;
                }
            }

            return false;
        }
    }
}


