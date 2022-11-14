using System.ComponentModel.DataAnnotations;

namespace CapgeminiCard.API.DTO
{
    public class ValidateCardDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int CardId { get; set; }
        [Required]
        public long Token { get; set; }
        [Required]
        public int CVV { get; set; }
    }
}
