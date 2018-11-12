using Pgs.Kanban.Domain.Dtos;

namespace Pgs.Kanban.Domain.Services.Interfaces
{
    public interface IBoardService
    {
        BoardDto CreateBoard(CreateBoardDto createBoardDto);
        bool EditBoard(EditBoardNameDto editBoardNameDto, int id);
        BoardDto GetBoard();
    }
}