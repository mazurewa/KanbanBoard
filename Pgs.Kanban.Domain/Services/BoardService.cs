using Microsoft.EntityFrameworkCore;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Models;
using System;
using System.Linq;

namespace Pgs.Kanban.Domain.Services
{
    public class BoardService
    {
        private readonly KanbanContext _context;

        public BoardService()
        {
            _context = new KanbanContext();
        }

        public BoardDto GetBoard()
        {
            var board = _context.Boards.Include(b => b.Lists).FirstOrDefault();

            if (board == null)
            {
                return null;
            }

            var boardDto = new BoardDto
            {
                Id = board.Id,
                Name = board.Name,
                Lists = board.Lists.Select(x => new ListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    BoardId = x.BoardId
                }).ToList()
            };

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

            var boardDto = new BoardDto()
            {
                Id = board.Id,
                Name = board.Name
            };

            return boardDto;
        }
    }
}
