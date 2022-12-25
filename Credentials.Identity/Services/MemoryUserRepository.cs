using Credentials.Identity.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Credentials.Identity.Services;

public sealed class MemoryUserRepository : IUserRepository
{
    private static readonly TimeSpan ExpiresIn = TimeSpan.FromMinutes(4);
    private readonly IMemoryCache _userMemoryCache;

    public MemoryUserRepository(IMemoryCache userMemoryCache)
    {
        _userMemoryCache = userMemoryCache;
    }

    public Task AddUserAsync(DbUser dbUser)
    {
        _userMemoryCache.Set(dbUser.UserName, dbUser, ExpiresIn);
        return Task.CompletedTask;
    }

    public Task<DbUser> GetUserByNameAsync(string userName)
    {
        return Task.FromResult(_userMemoryCache.Get<DbUser>(userName));
    }
}
