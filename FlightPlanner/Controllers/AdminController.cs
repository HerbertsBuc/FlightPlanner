using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]

    public class AdminController : ControllerBase
    {
        private static object Locker = new object();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            lock (Locker)
            {
                if (FlightStorage.FlightHasNullValues(flight))
                    return BadRequest();

                if (FlightStorage.SameAirport(flight))
                    return BadRequest();

                if (FlightStorage.ArrivalBeforeDeparture(flight))
                    return BadRequest();

                if (FlightStorage.FlightExists(flight))
                    return Conflict();

                FlightStorage.AddFlight(flight);

                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (Locker)
            {
                FlightStorage.DeleteFlight(id);

                return Ok();
            }
        }
    }
}
