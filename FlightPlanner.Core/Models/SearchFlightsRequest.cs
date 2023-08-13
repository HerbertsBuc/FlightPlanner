namespace FlightPlanner.Core.Models
{
    public class SearchFlightsRequest : Entity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
    }
}
