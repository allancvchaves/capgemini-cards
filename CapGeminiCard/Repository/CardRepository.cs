using CapGeminiCard.Context;
using CapGeminiCard.Entity;
using System.Threading.Tasks;

namespace CapGeminiCard.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly CardContext _cardContext;
        public CardRepository(CardContext cardContext)
        {
            _cardContext = cardContext;
        }

        public async Task<int> AddCard(CustomerCard card)
        {
            await _cardContext.CustomerCards.AddAsync(card);
            await _cardContext.SaveChangesAsync();
            return card.CardId;
        }

        public async Task<CustomerCard> GetCard(int id) => await _cardContext.CustomerCards.FindAsync(id);
    }
}
