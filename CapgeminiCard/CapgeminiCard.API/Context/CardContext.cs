using CapgeminiCard.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapgeminiCard.API.Context
{
    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions<CardContext> options) : base(options){}
        public DbSet<Card> Cards { get; set; }
    }
}
