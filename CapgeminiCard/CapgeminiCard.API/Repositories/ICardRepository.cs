using CapgeminiCard.API.Entities;

namespace CapgeminiCard.API.Repositories
{
    public interface ICardRepository
    {
        Task<int> Add(Card customerCard);
        Task<Card> GetCustomerCard(int id);
    }
}
