using DefenseByNight.API.Helpers;

namespace DefenseByNight.API.Dtos
{
    public class PowerDto: ITranslatable
    {
        public string PowerName { get; set; }

        public int Level { get; set; }

        public string System { get; set; }

        public string Description { get; set; }

        public FocusDto Focus { get; set; }

        public string FocusEffect { get; set; }

        public string ExceptionalSuccess { get; set; }
    }
}