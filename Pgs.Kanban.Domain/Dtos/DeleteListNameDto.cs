using System;
using System.Collections.Generic;
using System.Text;

namespace Pgs.Kanban.Domain.Dtos
{
    public class DeleteListNameDto
    {
        public int ListId { get; set; }
        public int BoardId { get; set; }
    }
}
