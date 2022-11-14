using System.ComponentModel.DataAnnotations;

namespace CapgeminiCard.API.Entities
{
    public class Card
    {        
        public int CustomerId { get; set; }
        [Key]
        public int CardId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
