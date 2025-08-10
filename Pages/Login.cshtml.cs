using ITasks.Models;
using ITasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITasks.Pages
{
    public class LoginPModel : PageModel
    {
        //View Model
        public LoginVModel vModel = new LoginVModel();

        //Binding Model
        [BindProperty]
        public LoginBModel bModel {  get; set; }

        ILoginService service { get; set; }

        public LoginPModel(ILoginService service)
        {
            this.service = service;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost() {
            vModel = new LoginVModel()
            {
                Username = bModel.Username,
                Password = bModel.Password,
                isAdmin = bModel.isAdmin
            };
            List<string> err = service.Validation(bModel.Username, bModel.Password, bModel.isAdmin, out User? user);

            if (err.Count > 0) {
                foreach (string error in err) {
                    ModelState.AddModelError("error:", error);
                }
                return Page();
            }
            service.Login(user!);
            return Redirect("/");
        }
    }

    public class LoginVModel {

        [Required]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required]
        [PasswordPropertyText(true)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Log in as adminstrator")]
        public bool isAdmin { get; set; } = false;
    }

    public class LoginBModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        public bool isAdmin { get; set; }
    }

}

