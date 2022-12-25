using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Credentials.Identity.Services;

internal sealed class DefaultSecretValidator : ISecretValidator
{
    public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
    {
        Console.WriteLine("2 - DefaultSecretValidator::ValidateAsync");
        return Task.FromResult(new SecretValidationResult { Success = true });
    }
}
