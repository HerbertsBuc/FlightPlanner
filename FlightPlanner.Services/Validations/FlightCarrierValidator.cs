using FlightPlanner.Core.Validations;
using FlightPlanner.Models;

namespace FlightPlanner.Services.Validations
{
    public class FlightCarrierValidator : IValidate
    {
        public bool IsValid( Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.Carrier);
        }
    }
}
