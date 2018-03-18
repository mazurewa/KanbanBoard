﻿using System.Collections.Generic;

namespace Pgs.Kanban.Domain.Dtos
{
    public class BoardDto
    {
        public BoardDto()
        {
            Lists = new List<ListDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ListDto> Lists { get; set; }
    }
}
