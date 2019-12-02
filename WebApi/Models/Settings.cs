using IdentityServer4.Models;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Settings : ISettings
    {
        public List<string> APIClientIDs { get; set; }
        public List<string> APIClientScopes { get; set; }
        public List<string> APIClientSecrets { get; set; }
        public List<string> APIResourceNames { get; set; }
        public string Authority { get; set; }

        public IEnumerable<ApiResource> GetApis()
        {
            var result = new List<ApiResource>();

            foreach (var apiResourceName in APIResourceNames)
                result.Add(new ApiResource(apiResourceName));

            return result;
        }

        public IEnumerable<Client> GetClients()
        {
            var result = new List<Client>();

            for (int i = 0; i < APIClientIDs.Count; i++)
            {
                result.Add(new Client
                {
                    ClientId = APIClientIDs[i],
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(APIClientSecrets[i].Sha256()) },
                    AllowedScopes = { APIClientScopes[i] }
                });
            }

            return result;
        }

        public IdentityResource[] GetResources()
        {
            return new IdentityResource[] { new IdentityResources.OpenId() };
        }
    }
}
