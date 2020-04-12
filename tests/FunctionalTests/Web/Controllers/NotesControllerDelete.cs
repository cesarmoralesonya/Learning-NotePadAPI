using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.Web.Controllers
{
    [Collection("SequentialDelete")]
    public class NotesControllerDelete: IClassFixture<WebTestFixture>
    {
        public NotesControllerDelete(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }
        public HttpClient Client { get; }

        [Fact]
        public async Task DeleteNoteCorrectly()
        {
            var response = await Client.DeleteAsync("/api/notes/1");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }
    }
}
