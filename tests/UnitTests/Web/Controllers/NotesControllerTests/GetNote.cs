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
    public class GetNote
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
            var result = await controller.GetNote(testNoteId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task FoundNote()
        {
            //Arrange
            int testSessionId = 1;
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(GetTestNote());
            var controller = new NotesController(mockRepo.Object);

            //Act
            var result = await controller.GetNote(testSessionId);

            //Asserts
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Note>(actionResult.Value);
            Assert.Equal(GetTestNote().Id, returnValue.Id);
            Assert.Equal(GetTestNote().Title, returnValue.Title);
            Assert.Equal(GetTestNote().Body, returnValue.Body);
            Assert.Equal(GetTestNote().DateTime, returnValue.DateTime);
        }

        #region snippet_GetTestNote
        private Note GetTestNote()
        {
            return new Note()
            {
                Id = 1,
                Title = "Test Title",
                Body = "Test Body",
                DateTime = DateTime.MinValue,
            };
        }
        #endregion
    }
}