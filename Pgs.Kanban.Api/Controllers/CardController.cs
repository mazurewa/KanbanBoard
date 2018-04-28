using Microsoft.AspNetCore.Mvc;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Services;

namespace Pgs.Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    public class CardController : Controller
    {
         private readonly CardService _cardService;

        public CardController()
        {
            _cardService = new CardService();
        }

        [HttpPost]
        public IActionResult AddCard([FromBody] AddCardDto addCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _cardService.AddCard(addCardDto);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult EditCardName([FromBody] EditCardDto editCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _cardService.EditCard(editCardDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteCard([FromBody] DeleteCardDto deleteCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _cardService.DeleteCard(deleteCardDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}