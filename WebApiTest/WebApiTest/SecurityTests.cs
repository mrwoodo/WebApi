using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace WebApiTest
{
    [TestClass]
    public class SecurityTests
    {
        [TestMethod]
        [TestCategory("Security")]
        public async Task IdentityServerAuthToken()
        {
            var helper = new HttpClientHelper();
            var authToken = await helper.GetAccessToken();

            Assert.IsFalse(authToken.IsError);
        }

        [TestMethod]
        [TestCategory("Security")]
        public async Task IdentityServerDiscoveryEndpoint()
        {
            var helper = new HttpClientHelper();
            var discoveryResponse = await helper.GetDiscoveryResponse();

            Assert.IsFalse(discoveryResponse.IsError);
        }

        [TestMethod]
        [TestCategory("Security")]
        public async Task InvalidClientCredentials()
        {
            var helper = new HttpClientHelper
            {
                ApiClientId = "wrong",
                ApiClientSecret = "wrong"
            };
            var authToken = await helper.GetAccessToken();

            Assert.IsTrue(authToken.IsError);
        }

        [TestMethod]
        [TestCategory("Security")]
        public async Task InvalidScops()
        {
            var helper = new HttpClientHelper
            {
                ApiScope = "wrong"
            };
            var authToken = await helper.GetAccessToken();

            Assert.IsTrue(authToken.IsError);
        }
    }
}
