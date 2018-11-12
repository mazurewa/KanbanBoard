using System;
using Microsoft.EntityFrameworkCore;
using Pgs.Kanban.Domain.Models;

namespace Pgs.Kanban.Domain
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions options) : base(options) { }

        public DbSet<Board> Boards { get; set; } 

        public DbSet<List> Lists { get; set; }

        public DbSet<Card> Cards { get; set; }
    }
}