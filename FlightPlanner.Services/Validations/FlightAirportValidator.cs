using System;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;

namespace FlightPlanner.Services.Validations
{
    public class FlightAirportValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return !string.Equals(flight?.From.AirportCode.Trim(), 
                flight?.To.AirportCode.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
