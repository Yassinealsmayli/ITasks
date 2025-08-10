using ITasks.Models;

namespace ITasks.Services
{
    public interface ILoginService
    {
        void Login(User user);

        List<string> Validation(string username, string password, bool isAdmin, out User? user);
    }

    public class LoginService:ILoginService
    {
        ICurrentUser _user;
        IUserService _userService;
        AppDbContext _dbContext;

        public LoginService(ICurrentUser user,AppDbContext dbContext,IUserService userServive)
        {
            _user = user;
            _dbContext = dbContext;
            _userService = userServive;
        }
        public void Login(User user) {
            _user.SetCurrentUser(user);
        }


        public List<string> Validation(string username, string password, bool isAdmin, out User? u)
        {
            List<string> err = new List<string>();
            Models.User? user = _userService.GetUser(username);
            if (user != null)
            {
                if (_dbContext.Admins.Find(user.UID) == null && isAdmin)
                {
                    err.Add(user.Username + "is not an Adminstrator!");
                }
                if (user.Password != password)
                {
                    err.Add("Wrong Username or password!");
                }
            }
            else
            {
                err.Add("This Account does'nt exist!");
            }
            u = user;
            return err;
        }
    }
}
