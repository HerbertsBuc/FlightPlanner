using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Data;

namespace FlightPlanner
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
