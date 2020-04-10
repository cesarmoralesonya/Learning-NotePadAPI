using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitTests.Builder;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.NoteRepositoryTests
{
    public class GetById
    {
        private readonly NotePadContext _notePadContext;

        private readonly NoteRepository _noteRepository;

        private NoteBuilder NoteBuilder { get; } = new NoteBuilder();

        private readonly ITestOutputHelper _output;

        public GetById(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NotePadContext>()
                .UseInMemoryDatabase(databaseName: "TestNotes")
                .Options;
            _notePadContext = new NotePadContext(dbOptions);
            _noteRepository = new NoteRepository(_notePadContext);
        }

        [Fact]
        public async Task GetExistingNote()
        {
            //Arranges
            var existingNote = NoteBuilder.WithDefaultValues();
            _notePadContext.Notes.Add(existingNote);
            _notePadContext.SaveChanges();
            int noteId = existingNote.Id;
            _output.WriteLine($"OrderId: {noteId}");

            //Acts
            var noteFromRepo = await _noteRepository.GetByIdAsync(noteId);

            //Asserts
            Assert.Equal(NoteBuilder.TestId, noteFromRepo.Id);
        }

    }
}
