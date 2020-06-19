using System;

namespace DefenseByNight.API.Dtos
{
    public class ChronicleDto
    {
        public string ChronicleKey { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}