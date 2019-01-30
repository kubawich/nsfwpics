using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
    public class AddModel : PageModel
    {
		//Models.Upload upload = new Models.Upload();
        [BindProperty]
        public IFormFile Upload { get; set; }

        [HttpPost]
		public IActionResult OnPost(IFormFile files)
        {
            var MaxId = new Models.Tools().MaxId(0) + 1;

			Models.Upload.instance.UploadToMain(MaxId, Upload, Request.Cookies["login"]);
			
            return  Redirect("/Add");
        }

		[HttpGet]
		public IActionResult OnGet()
		{
			if (Request.Cookies["user_loged_in"] == "true")
			{
				return Page();
			} else return Redirect("https://nsfwpics.pw/");
		}
	}
}