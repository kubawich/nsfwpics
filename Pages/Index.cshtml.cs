using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
    public class IndexModel : PageModel
    {
		NSFWpics.Models.Main main = new Models.Main();
		static NSFWpics.Models.Tools tools = new Models.Tools();
        List<NSFWpics.Models.Image> list = new List<NSFWpics.Models.Image>();

        public int Id { get; set; }
        public int PageIncrement { get; set; }
		public int MaxId { get; set; } = tools.MaxId(0) / 10;

        [BindProperty]
        public NSFWpics.Models.Image Image { get; set; }

		public IActionResult OnGet(int id)
		{
			if (id == 1)
			{
				return Redirect("/");
			}
			Id = id; 
			return Page();
		}
		public List<NSFWpics.Models.Image> List()
		{
			if (Request.Cookies["viewType"] == "images")
			{
				list =  main.GetImages(Id, list);
				MaxId = tools.MaxId(1) / 10;
				return list;
			}
			else if(Request.Cookies["viewType"] == "videos")
			{
				list = main.GetVideos(Id, list);
				MaxId = tools.MaxId(2) / 10;
				return list;
			}
			else
			{
				list = main.GetAll(Id, list);
				MaxId = tools.MaxId(0) / 10;
				return list;
			}
		}
    }
}
