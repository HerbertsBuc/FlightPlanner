using FlightPlanner.Core.Validations;
using Flight = FlightPlanner.Models.Flight;

namespace FlightPlanner.Services.Validations
{
    public class AirportValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight?.From != null & flight?.To != null;
        }
    }
}
