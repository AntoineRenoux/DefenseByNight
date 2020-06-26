using System.Collections.Generic;

namespace DefenseByNight.API.Dtos.GameDto
{
    public class ClanDto
    {
        public string ClanKey { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string QuickDescription { get; set; }
        public string History { get; set; }
        public string Organisation { get; set; }
        public string Weekness { get; set; }
        public int RarityClan { get; set; }
        public string Affiliate { get; set; }
        public List<DisciplineDto> Disciplines { get; set; }
    }
}