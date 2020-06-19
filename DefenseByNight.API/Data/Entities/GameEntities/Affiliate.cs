using System.ComponentModel.DataAnnotations;

namespace DefenseByNight.API.Data.Entities
{
    public class Affiliate
    {
        [Key]
        public string AffiliateKey { get; set; }

        [Required]
        public string Description { get; set; }

        public string Logo { get; set; }
    }
}