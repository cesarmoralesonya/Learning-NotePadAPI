using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitTests.Builder;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.NoteRepositoryTests
{
    public class UpdateNote
    {
        private readonly NotePadContext _notePadContext;

        private readonly NoteRepository _noteRepository;

        private NoteBuilder NoteBuilder { get; } = new NoteBuilder();

        private readonly ITestOutputHelper _output;

        public UpdateNote(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NotePadContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdateNote")
                .Options;
            _notePadContext = new NotePadContext(dbOptions);
            _noteRepository = new NoteRepository(_notePadContext);
        }

        [Fact]
        public async Task UpdateAllProperties()
        {
            //Arranges
            var NoteToUpdate = NoteBuilder.WithDefaultValues();
            var UpdateNote = NoteBuilder.UpdateDefaultValues();
            var noteId = NoteToUpdate.Id;
            _output.WriteLine($"Id: {noteId}");

            //Acts
            _notePadContext.Notes.Add(NoteToUpdate);
            _notePadContext.SaveChanges();
            await _noteRepository.UpdateAsync(UpdateNote);
            var dbNote = _notePadContext.Notes.Find(noteId);

            //Asserts
            Assert.Equal(NoteToUpdate.Id, dbNote.Id);
            Assert.NotEqual(NoteToUpdate.Title, dbNote.Title);
            Assert.NotEqual(NoteToUpdate.Body, dbNote.Body);
            Assert.NotEqual(NoteToUpdate.DateTime, dbNote.DateTime);
        }
    }
}
