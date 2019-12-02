using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace WebApiTest
{
    [TestClass]
    public class SearchTests
    {
        private const string API_SEARCH_PREFIX = "/api/v1/enrolment?";

        [TestMethod]
        [TestCategory("Search")]
        public async Task InvalidCharactersSearch()
        {
            var helper = new HttpClientHelper();
            await helper.SetBearer();

            var search = new SearchRequest { Surname = "#Smith#" };
            var response = await helper.GetAsync($"{API_SEARCH_PREFIX}{search}");

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        [TestCategory("Search")]
        public async Task NameTooLongSearch()
        {
            var helper = new HttpClientHelper();
            await helper.SetBearer();

            var search = new SearchRequest { Surname = "ReallyLongNameReallyLongNameReallyLongNameReallyLongNameReallyLongNameReallyLongNameReallyLongName" };
            var response = await helper.GetAsync($"{API_SEARCH_PREFIX}{search}");

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        [TestCategory("Search")]
        public async Task NotFoundSearch()
        {
            var helper = new HttpClientHelper();
            await helper.SetBearer();

            var search = new SearchRequest { Surname = "Jones" };
            var response = await helper.GetAsync($"{API_SEARCH_PREFIX}{search}");

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        [TestCategory("Search")]
        public async Task RegularSearch()
        {
            var helper = new HttpClientHelper();
            await helper.SetBearer();

            var search = new SearchRequest();
            var response = await helper.GetAsync($"{API_SEARCH_PREFIX}{search}");

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        [TestCategory("Search")]
        public async Task UnauthorisedSearch()
        {
            var helper = new HttpClientHelper();
            var search = new SearchRequest();
            var response = await helper.GetAsync($"{API_SEARCH_PREFIX}{search}");

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
