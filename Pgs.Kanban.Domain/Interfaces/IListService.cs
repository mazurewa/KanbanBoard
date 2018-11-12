using Pgs.Kanban.Domain.Dtos;

namespace Pgs.Kanban.Domain.Services.Interfaces
{
    public interface IListService
    {
        ListDto AddList(AddListDto addListDto);
        bool DeleteList(int id);
        bool EditList(EditListDto editListDto, int id);
    }
}