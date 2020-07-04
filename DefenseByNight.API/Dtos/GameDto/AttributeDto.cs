using System.Collections.Generic;

namespace DefenseByNight.API.Dtos.GameDto
{
    public class AttributeDto
    {
        public string AttributeKey { get; set; }

        public string AttributeName { get; set; }

        public string Description { get; set; }

        public List<FocusDto> Focus { get; set; }
    }
}