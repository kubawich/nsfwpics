using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace NSFWpics.Pages
{
    public class QueueModel : PageModel
    {
		NSFWpics.Models.Queue queue = new Models.Queue();
		static NSFWpics.Models.Tools tools = new Models.Tools();
		List<NSFWpics.Models.Image> list = new List<NSFWpics.Models.Image>();

		public int Id { get; set; }
        public int PageIncrement { get; set; }
		public int MaxId { get; set; } = tools.MaxId(3) / 10;

        [BindProperty]
        public NSFWpics.Models.Image Image { get; set; }

		public IActionResult OnGet(int id)
		{
			if (id == 1)
			{
				return Redirect("/queue");
			}
			Id = id;
			return Page();
		}
		public List<NSFWpics.Models.Image> List()
		{
			if (Request.Cookies["viewType"] == "images")
			{
				list =  queue.GetImages(Id, list);
				MaxId = tools.MaxId(3) / 10;
				return list;
			}
			else if(Request.Cookies["viewType"] == "videos")
			{
				list = queue.GetVideos(Id, list);
				MaxId = tools.MaxId(3) / 10;
				return list;
			}
			else
			{
				list = queue.GetAll(Id, list);
				MaxId = tools.MaxId(3) / 10;
				return list;
			}
		}
    }
}
