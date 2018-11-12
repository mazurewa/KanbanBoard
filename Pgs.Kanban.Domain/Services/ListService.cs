﻿using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Models;
using Pgs.Kanban.Domain.Services.Interfaces;

namespace Pgs.Kanban.Domain.Services
{
    public class ListService : IListService
    {
        private readonly KanbanContext _context;
        private readonly IMapper _mapper;

        public ListService(KanbanContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ListDto AddList(AddListDto addListDto)
        {
            if (!_context.Boards.Any(x => x.Id == addListDto.BoardId))
            {
                return null;
            }

            var list = new List
            {
                Name = addListDto.Name,
                BoardId = addListDto.BoardId
            };

            _context.Lists.Add(list);
            _context.SaveChanges();

            return ConstructListDto(list);
        }

        public bool EditList(EditListDto editListDto, int id)
        {
            if (!_context.Boards.Any(x => x.Id == editListDto.BoardId))
            {
                return false;
            }

            var list = _context.Lists.SingleOrDefault(x => x.Id == id);

            if (list == null || list.BoardId != editListDto.BoardId)
            {
                return false;
            }

            if (list.Name == editListDto.Name)
            {
                return true;
            }

            list.Name = editListDto.Name;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteList(int id)
        {
            var list = _context.Lists.SingleOrDefault(x => x.Id == id);

            if (list == null)
            {
                return false;
            }

            _context.Lists.Remove(list);
            return _context.SaveChanges() > 0;
        }

        private ListDto ConstructListDto(List list)
        {
            var listDto = new ListDto
            {
                Id = list.Id,
                BoardId = list.BoardId,
                Name = list.Name
            };

            return listDto;
        }
    }
}
