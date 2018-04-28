using System.Collections.Generic;

namespace Pgs.Kanban.Domain.Dtos
{
    public class ListDto
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Name { get; set; }
        public List<CardDto> Cards { get; set; }
    }
}