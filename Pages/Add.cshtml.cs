using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Renci.SshNet;

namespace NSFWpics.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public IFormFile Upload { get; set; }

        [HttpPost]
        [RequestSizeLimit(40000000)]
		public IActionResult OnPostAsync(IFormFile files)
        {
            var MaxId = DBEntities.DBEntity.Instance.MaxId() + 1;

			new DBEntities.DBEntity().UploadImgToDb(MaxId, Upload, Request.Cookies["login"]);
			
            return  Redirect("/Add");
        }

		string val = "true";
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