using AutoMapper;
using CapgeminiCard.API.DTO;
using CapgeminiCard.API.Entities;
using CapgeminiCard.API.Helper;
using CapgeminiCard.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CapgeminiCard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardsController(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] GenerateCardDTO cardDTO)
        {
            if (cardDTO.CardNumber <= 0)
                return BadRequest("Invalid card number (Card number cannot be 0 or negative).");
            if (cardDTO.CVV <= 0)
                return BadRequest("Invalid CVV (CVV cannot be 0 or negative).");
            if (cardDTO.CustomerId <= 0)
                return BadRequest("Invalid Customer ID.");
            if (cardDTO.CardNumber.ToString().Length > 16)
                return BadRequest("Invalid card number (Must be less than 16 digits).");
            if (cardDTO.CVV.ToString().Length > 5)
                return BadRequest("Invalid CVV (Must be less than 5 digits).");

            Card card = _mapper.Map<Card>(cardDTO);
            long token;
            try
            {
                token = new Token().Generate(cardDTO.CardNumber, cardDTO.CVV);
                card.CreationDate = DateTime.UtcNow;
                card.CardId = await _cardRepository.AddCard(card);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured \n" + ex);
            }

            return Ok(new
            {
                RegistrationDate = card.CreationDate,
                Token = token,
                CardId = card.CardId
            });
        }

        [HttpGet]
        public async Task<IActionResult> ValidateToken([FromQuery] ValidateCardDTO cardDTO)
        {
            if (cardDTO.CVV <= 0)
                return BadRequest("Invalid CVV (CVV cannot be 0 or negative).");
            if (cardDTO.CustomerId <= 0)
                return BadRequest("Invalid Customer ID.");
            if (cardDTO.CardId <= 0)
                return BadRequest("Invalid Card ID.");
            if (cardDTO.CVV.ToString().Length > 5)
                return BadRequest("Invalid CVV (Must be less than 5 digits).");

            Card card;
            bool isValid;

            try
            {
                card = await _cardRepository.GetCustomerCard(cardDTO.CardId);
                isValid = new Token().Validate(cardDTO, card);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred \n" + ex);
            }

            return Ok(new
            {
                Validated = isValid
            });
        }
    }
}
