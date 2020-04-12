using ApplicationCore.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.Web.Controllers
{
    [Collection("SequentialPost")]
    public class NotesControllerPost: IClassFixture<WebTestFixture>
    {
        public NotesControllerPost(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task PostNoteCorrectly()
        {
            var note = new Note("Test", "Test", DateTime.Now);
            var response = await Client.PostAsJsonAsync("api/notes", note);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
