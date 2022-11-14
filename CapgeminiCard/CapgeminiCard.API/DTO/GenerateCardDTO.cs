using System.ComponentModel.DataAnnotations;

namespace CapgeminiCard.API.DTO
{
    public class GenerateCardDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public long CardNumber { get; set; }
        [Required]
        public int CVV { get; set; }
    }
}
