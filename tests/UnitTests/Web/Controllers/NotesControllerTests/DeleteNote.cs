using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers.NotesControllerTests
{
    public class DeleteNote
    {
        [Fact]
        public async Task NotFound()
        {
            //Arrange
            int testNoteId = 123;
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testNoteId))
                .ReturnsAsync((Note)null);
            var controller = new NotesController(mockRepo.Object);

            //Act
            var result = await controller.DeleteNote(testNoteId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task NoteDeleted()
        {
            //Arrange
            int testNoteId = 1;
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testNoteId))
                .ReturnsAsync(DeleteTestNote());
            mockRepo.Setup(repo => repo.DeleteAsync(DeleteTestNote()));
            var controller = new NotesController(mockRepo.Object);

            //Act
            var result = await controller.DeleteNote(testNoteId);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Note>(actionResult.Value);
            Assert.Equal(DeleteTestNote().Id, returnValue.Id);
        }

        #region snippet_DeleteTestNote
        private Note DeleteTestNote()
        {
            return new Note()
            {
                Id = 1,
                Title = "Title",
                Body = "Body",
                DateTime = DateTime.Now,
            };
        }
        #endregion
    }
}
