using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitTests.Builder;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.NoteRepositoryTests
{
    public class DeleteEntity
    {
        private readonly NotePadContext _notePadContext;

        private readonly NoteRepository _noteRepository;

        private NoteBuilder NoteBuilder { get; } = new NoteBuilder();

        private readonly ITestOutputHelper _output;

        public DeleteEntity(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NotePadContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteNote")
                .Options;
            _notePadContext = new NotePadContext(dbOptions);
            _noteRepository = new NoteRepository(_notePadContext);
        }

        [Fact]
        public async Task CheckIsDeleted()
        {
            //Arranges
            var NoteToDelete = NoteBuilder.WithDefaultValues();
            var noteId = NoteToDelete.Id;
            _output.WriteLine($"Id: {noteId}");

            //Acts
            _notePadContext.Notes.Add(NoteToDelete);
            _notePadContext.SaveChanges();
            await _noteRepository.DeleteAsync(NoteToDelete);
            var dbNote = _notePadContext.Notes.Find(noteId);

            //Asserts
            Assert.Null(dbNote);
        }
    }
}
