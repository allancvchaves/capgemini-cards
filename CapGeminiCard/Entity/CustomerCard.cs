using System.ComponentModel.DataAnnotations;

namespace CapGeminiCard.Entity
{
    public class CustomerCard
    {
        public int CustomerId { get; set; }
        [Key]
        public int CardId { get; set; }        
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
