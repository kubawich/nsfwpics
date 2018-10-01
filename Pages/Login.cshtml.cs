using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public string LoginLogin { get; set; }
		[BindProperty]
		public string LoginPassword { get; set; }
		[BindProperty]
		public string RegisterLogin { get; set; }
		[BindProperty]
		public string RegisterPassword { get; set; }
		[BindProperty]
		public string RegisterMail { get; set; }

		[HttpPost, ActionName("login")]
		public IActionResult Login()
        {
			return RedirectToPage("Add.cshtml");
        }

		[HttpPost, ActionName("register")]
		public void Register()
		{

		}
	}
}