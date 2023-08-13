using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly ISearchFlightService _searchFlightService;
        private readonly IMapper _mapper;

        public CustomerController(IAirportService airportService, IMapper mapper, ISearchFlightService searchFlightService, IFlightService flightService)
        {
            _airportService = airportService;
            _mapper = mapper;
            _searchFlightService = searchFlightService;
            _flightService = flightService;
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
        public IActionResult SearchFlights(FlightPlanner.Core.Models.SearchFlightsRequest flightRequest)
        {
            var results = _searchFlightService.SearchFlights(flightRequest);

            if (results == null)
                return BadRequest();

            var pageResult = new PageResult();

            pageResult.Items = results;
            pageResult.TotalItems = results.Count();

            return Ok(pageResult);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFullFlight(id);
            if (flight == null)
                return NotFound();

            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }
    }
}
