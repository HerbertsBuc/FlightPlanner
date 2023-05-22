using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PageResult = FlightPlanner.Models.PageResult;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        public static PageResult _pageResult = new PageResult();
        public static Flight GetFlight(FlightPlannerDbContext context, int id)
        {
            return context.Flights.Include(f => f.From)
                .Include(f => f.To).SingleOrDefault(f => f.Id == id);
        }
        
        public static void DeleteFlight(FlightPlannerDbContext context, int id)
        {
            var flight = GetFlight(context, id);

            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }
        }

        public static bool FlightExists(FlightPlannerDbContext context, Flight flight)
        {
            return context.Flights.Any(f => f.From.AirportCode == flight.From.AirportCode &&
                                            f.From.City == flight.From.City &&
                                            f.From.Country == flight.From.Country &&
                                            f.To.AirportCode == flight.To.AirportCode &&
                                            f.To.City == flight.To.City &&
                                            f.To.Country == flight.To.Country &&
                                            f.Carrier == flight.Carrier &&
                                            f.DepartureTime == flight.DepartureTime &&
                                            f.ArrivalTime == flight.ArrivalTime);
        }

        public static bool FlightHasNullValues(Flight flight)
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

        public static bool SameAirport(Flight flight)
        {
            if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return true;
            }

            return false;
        }

        public static bool ArrivalBeforeDeparture(Flight flight)
        {
            DateTime departure = DateTime.Parse(flight.DepartureTime);
            DateTime arrival = DateTime.Parse(flight.ArrivalTime);

            if (departure >= arrival)
            {
                return true;
            }

            return false;
        }

        public static List<Airport> SearchAirports(FlightPlannerDbContext context, string airport)
        {
            if (string.IsNullOrEmpty(airport))
            {
                return null;
            }

            var airports = from f in context.Airports
                           where (f.Country.ToLower().Contains(airport.Trim().ToLower()) ||
                                  f.City.ToLower().Contains(airport.Trim().ToLower()) ||
                                  f.AirportCode.ToLower().Contains(airport.Trim().ToLower()))
                           select f;

            return airports.ToList();

        }

        public static PageResult SearchFlights(FlightPlannerDbContext context, SearchFlightsRequest flight)
        {
            _pageResult.Items = new List<Flight>();
            _pageResult.TotalItems = 0;

            if (flight.From == null || flight.To == null || flight.DepartureDate == null)
                return null;

            if (flight.From == flight.To)
                return null;

            _pageResult.Items = (from f in context.Flights
                                 where (f.From.AirportCode == flight.From && f.To.AirportCode == flight.To &&
                                        f.DepartureTime.Substring(0, 10) == flight.DepartureDate)
                                 select f).ToList();

            if (_pageResult.Items.Count != 0)
                _pageResult.TotalItems++;

            return _pageResult;
        }
    }
}
