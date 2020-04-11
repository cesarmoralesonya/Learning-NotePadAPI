using Infraestructure.Data;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.NoteRepositoryTests
{
    public class ListAll
    {
        private readonly NotePadContext _notePadContext;

        private readonly NoteRepository _noteRepository;

        private readonly ITestOutputHelper _output;

        public ListAll(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NotePadContext>()
                .UseInMemoryDatabase(databaseName: "TestListNotes")
                .Options;
            _notePadContext = new NotePadContext(dbOptions);
            NotePadContextSeed.Initialize(_notePadContext);
            _noteRepository = new NoteRepository(_notePadContext);
        }

        [Fact]
        public async Task CountSeedIsCorrect()
        {
            //Arrange
            _output.WriteLine($"the count of seed is: {NotePadContextSeed.Count}");

            //Act
            var listNotes = await _noteRepository.ListAllAsync();

            //Arrange
            Assert.Equal(NotePadContextSeed.Count, listNotes.Count);
        }
    }
}
