using ApplicationCore.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.Web.Controllers
{
    [Collection("SequentialUpdate")]
    public class NotesControllerPut: IClassFixture<WebTestFixture>
    {
        public NotesControllerPut(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task UpdateNoteCorrectly()
        {
            var note = PutNote();
            var response = await Client.PutAsJsonAsync($"api/notes/{note.Id}", note);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        #region snippet_PutTestNote
        private Note PutNote()
        {
            return new Note()
            {
                Id = 1,
                Title = "Title",
                Body = "Body",
                DateTime = DateTime.Now
            };
        }
        #endregion
    }
}
