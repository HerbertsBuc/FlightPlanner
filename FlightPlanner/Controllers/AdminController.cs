using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;
using FlightPlanner.Services;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]

    public class AdminController : BaseApiController
    {
        public AdminController(FlightPlannerDbContext context) : base(context) { }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {

            if (FlightStorage.FlightHasNullValues(flight) ||
                FlightStorage.SameAirport(flight) ||
                FlightStorage.ArrivalBeforeDeparture(flight))
            {
                return BadRequest();
            }

            if (FlightStorage.FlightExists(_context, flight))
            {
                return Conflict();
            }

            _context.Flights.Add(flight);
            _context.SaveChanges();

            return Created("", flight);
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(_context, id);

            return Ok();
        }

    }
}
