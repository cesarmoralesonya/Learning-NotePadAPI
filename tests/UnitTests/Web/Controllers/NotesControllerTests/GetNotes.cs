using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers.NotesControllerTests
{
    public class GetNotes
    {
        [Fact]
        public async Task ReturnIEnumerableNotes()
        {
            //Arrange
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(GetTestNotes());
            var controller = new NotesController(mockRepo.Object);

            //Act
            var result = await controller.GetNotes();

            //Asserts
            var lstResult = Assert.IsAssignableFrom<IEnumerable<Note>>(result);
            int count = 0;
            using (IEnumerator<Note> enumerator = lstResult.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    count++;
            }
            Assert.Equal(GetTestNotes().Count, count);
        }

        #region snippet_GetTestSessions
        private List<Note> GetTestNotes()
        {
            var notes = new List<Note>();
            notes.Add(new Note("TestTitle", "TestBody", DateTime.MinValue));
            notes.Add(new Note("TestTitle2", "TestBody2", DateTime.MaxValue));
            return notes;
        }
        #endregion
    }
}