using IdentityServer4.Models;
using System.Collections.Generic;

namespace WebApi.Models
{
    public interface ISettings
    {
        List<string> APIClientIDs { get; set; }
        List<string> APIClientScopes { get; set; }
        List<string> APIClientSecrets { get; set; }
        List<string> APIResourceNames { get; set; }
        string Authority { get; set; }

        IdentityResource[] GetResources();
        IEnumerable<ApiResource> GetApis();
        IEnumerable<Client> GetClients();
    }
}
