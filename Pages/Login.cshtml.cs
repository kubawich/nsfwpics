using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel'
    public class LoginModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel'
    {
		[BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.LoginLogin'
		public string LoginLogin { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.LoginLogin'
		[BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.LoginPassword'
		public string LoginPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.LoginPassword'
		[BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.RegisterLogin'
		public string RegisterLogin { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.RegisterLogin'
		[BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.RegisterPassword'
		public string RegisterPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.RegisterPassword'
		[BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.RegisterMail'
		public string RegisterMail { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.RegisterMail'

		[HttpPost, ActionName("login")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.Login()'
		public IActionResult Login()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.Login()'
        {
			return RedirectToPage("Add.cshtml");
        }

		[HttpPost, ActionName("register")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.Register()'
		public void Register()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'LoginModel.Register()'
		{

		}
	}
}