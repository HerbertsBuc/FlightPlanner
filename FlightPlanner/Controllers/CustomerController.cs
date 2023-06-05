using FlightPlanner.Data;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        private readonly IFlightPlannerDbContext _context;
        public CustomerController(IFlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            return Ok(); //FlightStorage.SearchAirports(_context, search));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest flight)
        {
           // var results = FlightStorage.SearchFlights(_context, flight);

           // if (results == null)
             //   return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
          //  var flight = FlightStorage.GetFlight(_context, id);
          //  if (flight == null)
           //     return NotFound();

            return Ok();
        }
    }
}
