using Microsoft.EntityFrameworkCore;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pgs.Kanban.Domain.Services
{
    public class CardService
    {
        private readonly KanbanContext _context;

        public CardService()
        {
            _context = new KanbanContext();
        }

        public CardDto AddCard(AddCardDto addCardDto)
        {
            if (!_context.Lists.Any(x => x.Id == addCardDto.ListId))
            {
                return null;
            }

            var card = new Card
            {
                Name = addCardDto.Name,
                ListId = addCardDto.ListId,
                Description = ""
            };

            _context.Cards.Add(card);
            var result = _context.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            var cardDto = new CardDto
            {
                Id = card.Id,
                ListId = card.ListId,
                Name = card.Name,
                Description = ""
            };

            return cardDto;
        }

        public bool EditCard(EditCardDto editCardDto)
        {
            var card = _context.Cards.SingleOrDefault(x => x.Id == editCardDto.CardId);

            if (card == null || (card.Name == editCardDto.Name && card.Description == editCardDto.Description))
            {
                return false;
            }

            card.Name = editCardDto.Name;
            card.Description = editCardDto.Description;
            _context.Entry(card).State = EntityState.Modified;

            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool DeleteCard(DeleteCardDto deleteCardDto)
        {
            var card = GetCard(deleteCardDto.CardId);

            if (card == null)
            {
                return false;
            }

            _context.Cards.Remove(card);
            var result = _context.SaveChanges();
            return result > 0;
        }

        private Card GetCard(int id)
        {
            return _context.Cards.SingleOrDefault(x => x.Id == id);
        }
    }
}