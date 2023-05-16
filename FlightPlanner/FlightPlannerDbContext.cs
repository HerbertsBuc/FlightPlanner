using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FlightPlanner
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions options) :base(options)
        {
        }
        
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

    }
}
