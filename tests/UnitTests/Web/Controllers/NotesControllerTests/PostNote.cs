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
    public class PostNote
    {
        [Fact]
        public async Task InvalidModel_BadRequest()
        {
            //Arranges
            var mockRepo = new Mock<INoteRepository>();
            var controller = new NotesController(mockRepo.Object);

            //Acts
            controller.ModelState.AddModelError("error", "some error");
            var result = await controller.PostNote(note: null);

            //Asserts
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task NoteCreated()
        {
            //Arranges
            var mockRepo = new Mock<INoteRepository>();
            mockRepo.Setup(repo => repo.AddAsync(PostTestNote()))
                .ReturnsAsync(PostTestNote());
            var controller = new NotesController(mockRepo.Object);

            //Acts
            var result = await controller.PostNote(PostTestNote());

            //Arranges
            var CreatedAtAction = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetNote", CreatedAtAction.ActionName);
            var value = Assert.IsType<Note>(CreatedAtAction.Value);
            Assert.Equal(PostTestNote().Id, value.Id);
        }

        #region snippet_PostTestNote
        private Note PostTestNote()
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
