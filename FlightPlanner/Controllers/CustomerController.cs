using System.Collections.Generic;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public CustomerController(IAirportService airportService, IMapper mapper)
        {
            _airportService = airportService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = _airportService.SearchAirports(search);
            List<AddAirportRequest> returnAirports = new List<AddAirportRequest>();

            foreach (Airport a in airports)
            {
                returnAirports.Add(_mapper.Map<AddAirportRequest>(a));
            }

            return Ok(returnAirports);
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
