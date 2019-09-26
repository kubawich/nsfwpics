using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddModel'
    public class AddModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddModel'
    {
        [BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddModel.Upload'
        public IFormFile Upload { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddModel.Upload'

        [HttpPost]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddModel.OnPost(IFormFile)'
		public IActionResult OnPost(IFormFile files)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddModel.OnPost(IFormFile)'
        {
            var MaxId = new Models.Tools().MaxId(3) + 1;

			new Models.Upload().UploadToQueue(MaxId, Upload, Request.Cookies["login"]);
			
            return  Redirect("/Add");
        }

		[HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddModel.OnGet()'
		public IActionResult OnGet()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddModel.OnGet()'
		{
			if (Request.Cookies["user_loged_in"] == "true")
			{
				return Page();
			} else return Redirect("https://nsfwpics.pw/");
		}
	}
}