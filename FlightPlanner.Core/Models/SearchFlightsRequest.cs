using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Models
{
    public class SearchFlightsRequest : Entity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
