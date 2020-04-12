using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers.NotesControllerTests
{
    public class PutNote
    {
        [Fact]
        public async Task InvalidModel_BadRequest()
        {
            //Arranges
            var mockRepo = new Mock<INoteRepository>();
            var controller = new NotesController(mockRepo.Object);

            //Acts
            controller.ModelState.AddModelError("error", "some error");
            var result = await controller.PutNote(id: 0, note: null);

            //Asserts
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DifferentId_BadRequest()
        {
            //Arranges
            var mockRepo = new Mock<INoteRepository>();
            var controller = new NotesController(mockRepo.Object);

            //Acts
            var result = await controller.PutNote(id: 2, note: PutTestNote());

            //Asserts
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task NoteUpdated()
        {
            //Arranges
            var mockRepo = new Mock<INoteRepository>();            
            mockRepo.Setup(repo => repo.UpdateAsync(PutTestNote()));
            var controller = new NotesController(mockRepo.Object);

            //Acts
            var result = await controller.PutNote(PutTestNote().Id, PutTestNote());

            //Asserts
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task NotFound()
        {
            //Arranges
            var mockRepo = new Mock<INoteRepository>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Note>()))
                    .Throws(new DbUpdateConcurrencyException("Test"));
                    
            var controller = new NotesController(mockRepo.Object);

            //Acts
            var result = await controller.PutNote(PutTestNote().Id, PutTestNote());

            //Asserts
            Assert.IsType<NotFoundResult>(result);
        }

        #region snippet_PutTestNote
        private Note PutTestNote()
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
