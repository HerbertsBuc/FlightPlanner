using FlightPlanner.Core.Validations;
using FlightPlanner.Models;

namespace FlightPlanner.Services.Validations
{
    public class AirportPropsValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.From.AirportCode) &&
                   !string.IsNullOrEmpty(flight?.From.City) &&
                   !string.IsNullOrEmpty(flight?.From.Country) &&
                   !string.IsNullOrEmpty(flight?.To.AirportCode) &&
                   !string.IsNullOrEmpty(flight?.To.City) &&
                   !string.IsNullOrEmpty(flight?.To.Country);
        }
    }
}
