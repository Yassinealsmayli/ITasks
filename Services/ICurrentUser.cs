using ITasks.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ITasks.Services
{
    public interface ICurrentUser
    {
        void SetCurrentUser(User user);

        User? GetCurrentUser();
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IMemoryCache _cache;

        public CurrentUser(IMemoryCache cache) {  _cache = cache; }
        public User? GetCurrentUser()
        {
            _cache.TryGetValue("Current", out User? currentUser);
            return currentUser;
        }

        public void SetCurrentUser(User user)
        {
            _cache.Set<User>("Current",user);
        }
    }
}

