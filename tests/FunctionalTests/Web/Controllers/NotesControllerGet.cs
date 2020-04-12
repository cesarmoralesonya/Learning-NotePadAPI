using ApplicationCore.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.Web.Controllers
{
    [Collection("SequentialGet")]
    public class NotesControllerGet : IClassFixture<WebTestFixture>
    {
        public NotesControllerGet(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsListNotes()
        {
            var response = await Client.GetAsync("/api/notes");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<List<Note>>(stringResponse);

            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task ReturnsNote()
        {
            var response = await Client.GetAsync("/api/notes/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Note>(stringResponse);

            Assert.NotNull(model);
        }
    }
}
