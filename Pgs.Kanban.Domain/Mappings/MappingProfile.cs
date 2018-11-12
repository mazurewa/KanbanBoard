using AutoMapper;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Models;

namespace Pgs.Kanban.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Board, BoardDto>();
            CreateMap<List, ListDto>();
            CreateMap<Card, CardDto>();
        }
    }
}
