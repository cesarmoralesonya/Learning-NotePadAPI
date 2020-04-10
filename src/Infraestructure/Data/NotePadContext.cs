using Microsoft.EntityFrameworkCore;

using ApplicationCore.Entities;

namespace Infraestructure.Data
{
    public class NotePadContext : DbContext
    {
        public NotePadContext(DbContextOptions<NotePadContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }        
    }
}