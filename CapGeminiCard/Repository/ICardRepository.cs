using CapGeminiCard.Entity;

namespace CapGeminiCard.Repository
{
    public interface ICardRepository
    {
        Task<int> AddCard(CustomerCard card);
        Task<CustomerCard> GetCard(int id);
    }
}
