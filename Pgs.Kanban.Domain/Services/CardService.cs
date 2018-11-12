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
    public class CardService : ICardService
    {
        private readonly KanbanContext _context;
        private readonly IMapper _mapper;

        public CardService(KanbanContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CardDto AddCard(AddCardDto addCardDto)
        {
            if (!ListExists(addCardDto.ListId))
            {
                return null;
            }

            var card = new Card
            {
                Name = addCardDto.Name,
                ListId = addCardDto.ListId
            };

            _context.Cards.Add(card);
            var resultOfAdding = _context.SaveChanges();

            if (resultOfAdding == 0)
            {
                return null;
            }

            var cardDto = _mapper.Map<CardDto>(card);

            return cardDto;
        }

        public bool EditCard(EditCardDto editCardDto)
        {
            var card = GetCard(editCardDto.Id);
            if (card == null)
            {
                return false;
            }

            card.Name = editCardDto.Name;
            _context.Entry(card).State = EntityState.Modified;

            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool DeleteCard(int id)
        {
            var card = GetCard(id);
            if (card == null)
            {
                return false;
            }

            _context.Cards.Remove(card);
            var result = _context.SaveChanges();
            return result > 0;
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(x => x.Id == id);
        }

        private Card GetCard(int id)
        {
            var list = _context.Cards.FirstOrDefault(x => x.Id == id);
            return list;
        }
    }
}