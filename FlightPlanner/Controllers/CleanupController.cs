using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : BaseApiController
    {
        private static object Locker = new object();
        public CleanupController(FlightPlannerDbContext context) : base(context) { }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Flights");
            _context.Database.ExecuteSqlRaw("DELETE FROM Airports");
            _context.SaveChanges();

            return Ok();
        }
    }
}
