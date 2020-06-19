using System;

namespace DefenseByNight.API.Models
{
    public class ChronicleViewModel
    {
        public string ChronicleKey { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}