using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Test.Models.Dto;
using RapidPay.Test.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapidPay.Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-card")] 
        public IActionResult CreateCard([FromBody] CardDto card)
        {
            var createdCard = _cardService.CreateCard(card);
            return Ok(createdCard);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-balance/{cardNumber}")]
        public IActionResult GetBalance(double cardNumber)
        {
            var balance = _cardService.GetCardBalance(cardNumber);
            return Ok(balance);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("pay")]
        public IActionResult PayWithCard([FromBody] CardTransactionDto cardTransaction)
        {
            var card = _cardService.PayWithCard(cardTransaction);
            return Ok(card);
        }
    }
}
