using Microsoft.AspNetCore.Mvc;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Services.Interfaces;

namespace Pgs.Kanban.Api.Controllers
{
    [Route("api/Card")]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public IActionResult AddCard([FromBody] AddCardDto addCardDto)
        {
            var result = _cardService.AddCard(addCardDto);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult EditCard([FromBody] EditCardDto editCardDto)
        {
            var result = _cardService.EditCard(editCardDto);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCard(int id)
        {
            var result = _cardService.DeleteCard(id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }    
}