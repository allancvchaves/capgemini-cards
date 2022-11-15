using CapgeminiCard.API.DTO;
using CapgeminiCard.API.Entities;
using CapgeminiCard.API.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapgeminiCard.Tests
{
    public class TokenTests
    {
        private readonly Token _token;

        public TokenTests()
        {
            _token = new Token();
        }
        #region Generate
        [Test]
        public void GenerateToken_ReturnOK()
        {
            long cardNumber = 4567;
            int cvv = 123;

            var result = _token.Generate(cardNumber, cvv);

            Assert.That(result, Is.EqualTo(5674));
        }
        #endregion
        #region Validate
        [Test]
        public void ValidateToken_ReturnTrue()
        {
            ValidateCardDTO cardDTO = new ValidateCardDTO { CardId = 1, CustomerId = 1, CVV = 123, Token = 5674 };
            Card validCard = new Card { CardId = 1, CustomerId = 1, CVV = 123, CardNumber = 4567, CreationDate = DateTime.UtcNow };

            var result = _token.Validate(cardDTO, validCard);
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateToken_ReturnFalse()
        {
            ValidateCardDTO cardDTO = new ValidateCardDTO { CardId = 1, CustomerId = 1, CVV = 123, Token = 5664 };
            Card validCard = new Card { CardId = 1, CustomerId = 1, CVV = 22, CardNumber = 3523, CreationDate = DateTime.UtcNow };

            var act = _token.Validate(cardDTO, validCard);
            Assert.That(act, Is.False);
        }
        [Test]
        public void ValidateToken_ReturnFalse2()
        {
            ValidateCardDTO cardDTO = new ValidateCardDTO { CardId = 1, CustomerId = 1, CVV = 123, Token = 5664 };
            Card validCard = new Card { CardId = 1, CustomerId = 1, CVV = 22, CardNumber = 3523, CreationDate = DateTime.UtcNow.AddHours(-2) };

            var act = _token.Validate(cardDTO, validCard);
            Assert.That(act, Is.False);
        }
        #endregion
    }
}
