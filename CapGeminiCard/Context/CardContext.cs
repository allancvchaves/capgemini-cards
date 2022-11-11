using CapGeminiCard.Entity;
using Microsoft.EntityFrameworkCore;

namespace CapGeminiCard.Context
{
    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CustomerCard> CustomerCards { get; set; }
    }
}
