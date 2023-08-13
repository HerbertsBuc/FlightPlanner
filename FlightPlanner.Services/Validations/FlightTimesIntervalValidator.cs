using System;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;

namespace FlightPlanner.Services.Validations
{
    public class FlightTimesIntervalValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            DateTime.TryParse(flight.DepartureTime, out var departureTime);
            DateTime.TryParse(flight.ArrivalTime, out var arrivalTime);

            return arrivalTime > departureTime;
        }
    }
}
