using AutoMapper;
using CapgeminiCard.API.Controllers;
using CapgeminiCard.API.DTO;
using CapgeminiCard.API.Entities;
using CapgeminiCard.API.Mapper;
using CapgeminiCard.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapgeminiCard.Tests
{
    

    public class ControllerTests
    {
        private readonly IMapper _mapper;
        private readonly CardsController _controller;
        private Mock<ICardRepository> _repository;

        public ControllerTests()
        {
            _repository = new Mock<ICardRepository>();
            var mapperConfig = new MapperConfiguration(
                x =>
                {
                    x.AddProfile(new CardMapping());
                });
            _mapper = mapperConfig.CreateMapper();
            _controller = new CardsController(_repository.Object, _mapper);
        }

        #region OkTests
        [Test]
        public async Task AddCard_ReturnOK()
        {
            var dto = new GenerateCardDTO { CardNumber = 4567, CustomerId = 1, CVV = 123 };

            var result = await _controller.AddCard(dto);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task ValidateCard_ReturnOk()
        {
            var cardDto = new ValidateCardDTO { CustomerId = 1, CardId = 1, CVV = 123, Token = 5674 };
            var returnValidationDTO = new { Validated = true };
            var card = new Card { CardId = 1, CardNumber = 4567, CustomerId = 1, CreationDate = DateTime.UtcNow, CVV = 123 };
            _repository.Setup(x => x.GetCustomerCard(1)).ReturnsAsync(card);

            var result = await _controller.ValidateToken(cardDto);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region ErrorTests
        [Test]
        [TestCaseSource(nameof(AddCard_InvalidFields))]
        public async Task AddCard_ReturnError(GenerateCardDTO cardDTO)
        {
            var result = await _controller.AddCard(cardDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        [TestCaseSource(nameof(ValidateCard_InvalidFields))]
        public async Task ValidateCard_ReturnError(ValidateCardDTO validateCardDTO)
        {
            var result = await _controller.ValidateToken(validateCardDTO);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region TestData
        public static IEnumerable<TestCaseData> AddCard_InvalidFields
        {
            get
            {
                yield return new TestCaseData(new GenerateCardDTO { CardNumber = 5666576543677432234, CustomerId = 1, CVV = 111 });
                yield return new TestCaseData(new GenerateCardDTO { CardNumber = 2222, CustomerId = 1, CVV = 999999 });
                yield return new TestCaseData(new GenerateCardDTO { CardNumber = 0, CustomerId = 1, CVV = 1234 });
                yield return new TestCaseData(new GenerateCardDTO { CardNumber = 4567, CustomerId = -1, CVV = 1234 });
                yield return new TestCaseData(new GenerateCardDTO { CardNumber = 4567, CustomerId = 1, CVV = -33 });
            }
        }
        public static IEnumerable<TestCaseData> ValidateCard_InvalidFields
        {
            get
            {
                yield return new TestCaseData(new ValidateCardDTO { CustomerId = 1, CardId = 1, Token = 1, CVV = 666666 });
                yield return new TestCaseData(new ValidateCardDTO { CustomerId = -2, CardId = 1, Token = 1, CVV = 1 });
                yield return new TestCaseData(new ValidateCardDTO { CustomerId = 1, CardId = -2, Token = 1, CVV = 1 });
                yield return new TestCaseData(new ValidateCardDTO { CustomerId = 1, CardId = 1, Token = 1, CVV = -2 });
            }
        }
        #endregion

    }
}
