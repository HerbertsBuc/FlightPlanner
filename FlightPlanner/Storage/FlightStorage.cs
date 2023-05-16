using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using PageResult = FlightPlanner.Models.PageResult;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        public static PageResult _pageResult = new PageResult();
        public static Flight GetFlight(FlightPlannerDbContext context, int id)
        {
            return context.Flights.SingleOrDefault(f => f.Id == id);
        }

        public static Flight AddFlight(FlightPlannerDbContext context, Flight flight)
        {
            //flight.Id = _id++;
            context.Flights.Add(flight);
            return flight;
        }

        public static void DeleteFlight(FlightPlannerDbContext context, int id)
        {
            var flight = GetFlight(context, id);

            if (flight != null)
                context.Flights.Remove(flight);
        }

        public static bool FlightExists(FlightPlannerDbContext context, Flight flight)
        {
            foreach (Flight f in context.Flights)
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

            if (flight.From == flight.To ||
                flight.From.City.ToLower() == flight.To.City.ToLower() ||
                flight.From.Country.ToLower() == flight.To.Country.ToLower() ||
                flight.From.AirportCode.ToLower() == flight.To.AirportCode.ToLower())
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
            lock (context.Flights)
            {
                List<Airport> airports = new List<Airport>();

                if (string.IsNullOrEmpty(airport))
                {
                    return null;
                }

                foreach (Flight f in context.Flights)
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

        public static PageResult SearchFlights(FlightPlannerDbContext context, SearchFlightsRequest flight)
        {
            lock (context.Flights)
            {
                _pageResult.Items = new List<Flight>();
                _pageResult.TotalItems = 0;

                if (flight.From == null || flight.To == null || flight.DepartureDate == null)
                    return null;

                if (flight.From == flight.To)
                    return null;

                foreach (Flight f in context.Flights)
                {
                    if (f.From.AirportCode == flight.From && f.To.AirportCode == flight.To &&
                        f.DepartureTime.Substring(0, 10) == flight.DepartureDate)
                    {
                        _pageResult.TotalItems++;
                        _pageResult.Items.Add(f);
                    }
                }
            }

            return _pageResult;
        }
    }
}
