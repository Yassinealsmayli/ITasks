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
        IUserService _userService;

        public LoginService(IUserService userService)
        {
            _userService = userService;
        }
        public void Login(User user) {
            _userService.SetCurrentUser(user);
        }


        public List<string> Validation(string username, string password, bool isAdmin, out User? u)
        {
            List<string> err = new List<string>();
            Models.User? user = _userService.GetUser(username);
            if (user != null)
            {
                if (user.IsAdmin != isAdmin)
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
                err.Add("This Account dsnt exist!");
            }
            u = user;
            return err;
        }
    }
}
