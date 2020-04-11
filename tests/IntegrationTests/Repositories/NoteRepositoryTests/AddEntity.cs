using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitTests.Builder;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.NoteRepositoryTests
{
    public class AddEntity
    {
        private readonly NotePadContext _notePadContext;
        
        private readonly NoteRepository _noteRepository;

        private NoteBuilder NoteBuilder { get; }  = new NoteBuilder();

        private readonly ITestOutputHelper _output;

        public AddEntity(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NotePadContext>()
                .UseInMemoryDatabase(databaseName: "TestAddNote")
                .Options;
            _notePadContext = new NotePadContext(dbOptions);
            _noteRepository = new NoteRepository(_notePadContext);
        }

        [Fact]
        public async Task CheckEntityIsCorrect()
        {
            //Arranges
            var note = NoteBuilder.WithDefaultValues();

            //Acts
            await _noteRepository.AddAsync(note);
            var noteFromDb = await _notePadContext.Notes.FindAsync(note.Id);

            //Asserts
            Assert.Equal(note.Id, noteFromDb.Id);
            Assert.Equal(note.Title, noteFromDb.Title);
            Assert.Equal(note.Body, noteFromDb.Body);
            Assert.Equal(note.DateTime, noteFromDb.DateTime);
        }

    }
}
