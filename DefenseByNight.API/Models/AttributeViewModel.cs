using System.Collections.Generic;

namespace DefenseByNight.API.Models
{
    public class AttributeViewModel
    {
        public string AttributeKey { get; set; }

        public string AttributeName { get; set; }

        public string Description { get; set; }

        public List<FocusViewModel> Focus { get; set; }
    }
}