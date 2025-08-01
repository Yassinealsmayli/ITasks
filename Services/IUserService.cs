using ITasks.Models;
using System.Threading.Tasks;

namespace ITasks.Services
{
    public interface IUserService
    {
        void SetCurrentUser(User user);

        User? GetCurrentUser();

        ValueTask<int> AddUser(User user);

        ValueTask<int> UpdateUser(int UID, User user);

        ValueTask<User?> GetUser(int UID);

        ValueTask<User?> DeleteUser(int UID);
    }

    public class UserService : IUserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context) { _context = context; }
        public async ValueTask<int> AddUser(User user)
        {
            int uuid = (await _context.Users.AddAsync(user)).Entity.UID;
            await _context.SaveChangesAsync();
            return uuid;
        }

        public async ValueTask<User?> DeleteUser(int UID)
        {
            if(await _context.Users.FindAsync(UID) == null)return null;
            User user = _context.Users.Remove(await _context.Users.FindAsync(UID)).Entity;
            await _context.SaveChangesAsync();
            return user;
        }

        public User? GetCurrentUser()
        {
            return AppDbContext.currentUser;
        }

        public async ValueTask<User?> GetUser(int UID)
        {
            return await _context.Users.FindAsync(UID);
        }

        public void SetCurrentUser(User user)
        {
            AppDbContext.currentUser = user;
        }

        public async ValueTask<int> UpdateUser(int UID, User user)
        {
            user.UID = UID;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return UID;
        }
    }
}
