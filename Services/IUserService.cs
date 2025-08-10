using ITasks.Models;
using System.Threading.Tasks;

namespace ITasks.Services
{
    public interface IUserService
    {

        ValueTask<int> AddUser(User user);

        ValueTask<int> UpdateUser(int UID, User user);

        ValueTask<User?> GetUser(int UID);

        User? GetUser(string Username);

        ValueTask<User?> DeleteUser(int UID);
    }

    public class UserService : IUserService
    {
        private AppDbContext _context;


        public UserService(AppDbContext context) {
            _context = context;
            _context.SaveChanges();
        }
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

        public async ValueTask<User?> GetUser(int UID)
        {
            return await _context.Users.FindAsync(UID);
        }

        public User? GetUser(string Username)
        {
            if (_context.Users.Where(u => u.Username == Username).Count() != 0)
                return _context.Users.Where(u => u.Username == Username).FirstOrDefault();
            else return null;
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
