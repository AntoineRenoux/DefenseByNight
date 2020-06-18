namespace DefenseByNight.API.Dtos.GameDto
{
    public class CharacterDto
    {
        public string CharacterKey { get; set; }

        public string Name { get; set; }

        public AffiliateDto Sect { get; set; }
    }
}