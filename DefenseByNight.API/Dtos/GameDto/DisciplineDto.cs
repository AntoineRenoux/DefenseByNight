using System.Collections.Generic;
using DefenseByNight.API.Helpers;

namespace DefenseByNight.API.Dtos
{
    public class DisciplineDto: ITranslatable
    {
        public string DisciplineKey { get; set; }

        public string DisciplineName { get; set; }

        public string Description { get; set; }

        public string TestScore { get; set; }

        public ICollection<PowerDto> Powers { get; set; }

    }
}