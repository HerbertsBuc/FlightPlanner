using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        bool FlightExists(Flight flight);
        bool FlightHasNullValues(Flight flight);
        bool SameAirport(Flight flight);
        bool ArrivalBeforeDeparture(Flight flight);
        Flight GetFullFlight(int id);
    }
}
