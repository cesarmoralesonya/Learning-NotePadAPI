using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitTests.Builder;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.NoteRepositoryTests
{
    public class UpdateEntity
    {
        private readonly NotePadContext _notePadContext;

        private readonly NoteRepository _noteRepository;

        private NoteBuilder NoteBuilder { get; } = new NoteBuilder();

        private readonly ITestOutputHelper _output;

        public UpdateEntity(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NotePadContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdateNote")
                .EnableSensitiveDataLogging()
                .Options;
            _notePadContext = new NotePadContext(dbOptions);
            _noteRepository = new NoteRepository(_notePadContext);
        }

        [Fact]
        public async Task UpdateAllProperties()
        {
            //Arranges
            var NoteToUpdate = NoteBuilder.WithDefaultValues();
            var UpdateNote = NoteBuilder.UpdateTitleValue();

            //Acts
            _notePadContext.Notes.Add(NoteToUpdate);
            _notePadContext.SaveChanges();
            _output.WriteLine($"Note to update Id: {NoteToUpdate.Id}");
            await _noteRepository.UpdateAsync(UpdateNote);
            var dbNote = _notePadContext.Notes.Find(NoteToUpdate.Id);

            //Asserts
            Assert.NotEqual(NoteToUpdate.Title, dbNote.Title);
        }
    }
}
