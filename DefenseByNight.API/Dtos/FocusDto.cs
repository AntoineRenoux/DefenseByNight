using DefenseByNight.API.Helpers;

namespace DefenseByNight.API.Dtos
{
    public class FocusDto: ITranslatable
    {
        public string FocusKey { get; set; }

        public string FocusName { get; set; }

        public string Description { get; set; }
    }
}