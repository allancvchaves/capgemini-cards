using CapgeminiCard.API.Context;
using CapgeminiCard.API.Entities;

namespace CapgeminiCard.API.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly CardContext _context;
        public CardRepository(CardContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Card customerCard)
        {
            await _context.Cards.AddAsync(customerCard);
            await _context.SaveChangesAsync();
            return customerCard.CardId;
        }

        public async Task<Card> GetCustomerCardAsync(int id) => await _context.Cards.FindAsync(id);
    }
}