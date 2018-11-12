using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Models;
using Pgs.Kanban.Domain.Services.Interfaces;

namespace Pgs.Kanban.Domain.Services
{
    public class BoardService : IBoardService
    {
        private readonly KanbanContext _context;
        private readonly IMapper _mapper;

        public BoardService(KanbanContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BoardDto GetBoard()
        {
            var board = _context.Boards
                .Include(b => b.Lists)
                .ThenInclude(c => c.Cards)
                .LastOrDefault();

            if (board == null)
            {
                return null;
            }

            var boardDto = _mapper.Map<BoardDto>(board);

            return boardDto;
        }

        public BoardDto CreateBoard(CreateBoardDto createBoardDto)
        {
            var board = new Board()
            {
                Name = createBoardDto.Name
            };

            _context.Boards.Add(board);
            _context.SaveChanges();

            return ConstructBoardDto(board);
        }

        public bool EditBoard(EditBoardNameDto editBoardNameDto, int id)
        {
            var board = GetBoardById(id);
            if (board == null)
            {
                return false;
            }

            if (board.Name == editBoardNameDto.Name)
            {
                return true;
            }
            board.Name = editBoardNameDto.Name;
            return _context.SaveChanges() > 0;
        }

        private BoardDto ConstructBoardDto(Board board)
        {
            return new BoardDto()
            {
                Id = board.Id,
                Name = board.Name
            };
        }

        private Board GetBoardById(int id)
        {
            return _context.Boards.SingleOrDefault(x => x.Id == id);
        }
    }
}