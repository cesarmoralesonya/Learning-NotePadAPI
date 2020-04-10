using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infraestructure.Data.Repositories
{
    public class NoteRepository : EfRepository<Note>, INoteRepository
    {
        public NoteRepository(NotePadContext dbContext) : base(dbContext)
        {
        }
    }
}
