using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Credentials.Identity.Services;

internal sealed class CredentialsStore : IClientStore
{
    public Task<Client> FindClientByIdAsync(string clientId)
    {
        //First Call.
        Console.WriteLine($"CredentialsStore::FindClientByIdAsync - ClientId: {clientId}");

        return Task.FromResult(
            new Client
            {
                ClientId = "123",
                ClientSecrets =
                {
                     new Secret { Value = "345" } // needed for token validation.
                },
                 
                Claims =
                {
                    new ClientClaim("Role", "Admin"),
                    new ClientClaim("Role", "DefaultUser")
                },
                ClientClaimsPrefix = string.Empty, // override default prefix.
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "test-1", "test-2", "test-3" } // add only scopes defined in the array in AppScope in the Program.cs -> GetApiScope
            });
    }
}
