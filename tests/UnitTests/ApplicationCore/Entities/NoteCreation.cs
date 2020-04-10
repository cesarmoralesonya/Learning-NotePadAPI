using UnitTests.Builder;
using Xunit;

namespace UnitTests.ApplicationCore.Entities
{
    public class NoteCreation
    {
        [Fact]
        public void NotePropertiesIsCorrect()
        {
            //Arrange
            var builder = new NoteBuilder();
            //Act
            var note = builder.WithDefaultValues();
            
            //Assert
            Assert.Equal(note.Id, builder.TestId);
            Assert.Equal(note.Title, builder.TestTitle);
            Assert.Equal(note.Body, builder.TestBody);
            Assert.Equal(note.DateTime, builder.TestDateTime);
        }
    }
}
