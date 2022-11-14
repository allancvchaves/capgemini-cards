using CapgeminiCard.API.DTO;
using CapgeminiCard.API.Entities;

namespace CapgeminiCard.API.Helper
{
    public class Token
    {
        public long Generate(long cardNumber, int cvv)
        {
            string cardString = cardNumber.ToString();
            string lastFour = cardString.Substring(cardString.Length - 4);

            int[] lastFourArray = lastFour.Select(n => int.Parse(n.ToString())).ToArray();

            var temp = lastFourArray[0];
            for (int i = 0; i < cvv; i++)
            {
                lastFourArray[i] = lastFourArray[i + 1];
            }
            lastFourArray[lastFourArray.Length - 1] = temp;

            return long.Parse(string.Concat(lastFourArray));
        }

        public bool Validate(ValidateCardDTO cardToValidate, Card validCard)
        {
            if (validCard.CreationDate < DateTime.UtcNow.AddMinutes(-30)
                || cardToValidate.CustomerId != validCard.CustomerId)
                return false;

            var token = Generate(validCard.CardNumber, cardToValidate.CVV);

            return token == cardToValidate.Token;
        }

    }
}