namespace DefenseByNight.API.Dtos.GameDto
{
    public class CharacterDto
    {
        public string CharacterKey { get; set; }

        public string Name { get; set; }

        public AffiliateDto Sect { get; set; }

        public ChronicleDto Chronicle { get; set; }

        public int PhysicalAttribut { get; set; }

        public int SocialAttribut { get; set; }

        public int MentalAttribut { get; set; }
    }
}