using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]

    public class AdminController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private static object Locker = new object();
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidate> _validators;

        public AdminController(IFlightService flightService, IMapper mapper, IEnumerable<IValidate> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            if (flight == null)
                return NotFound();

            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            lock (Locker)
            {
                var flight = _mapper.Map<Flight>(request);

                if (!_validators.All(validator => validator.IsValid(flight)))
                    return BadRequest();

                if (_flightService.FlightExists(flight))
                    return Conflict();

                _flightService.Create(flight);

                return Created("", _mapper.Map<AddFlightRequest>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (Locker)
            {
                var flight = _flightService.GetFullFlight(id);

                if (flight != null)
                    _flightService.Delete(flight);

                return Ok();
            }
        }
    }
}
