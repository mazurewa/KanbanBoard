using Pgs.Kanban.Domain.Dtos;

namespace Pgs.Kanban.Domain.Services.Interfaces
{
    public interface ICardService
    {
        CardDto AddCard(AddCardDto addCardDto);
        bool DeleteCard(int id);
        bool EditCard(EditCardDto editCardDto);
    }
}