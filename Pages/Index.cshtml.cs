using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel'
    public class IndexModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel'
    {
		NSFWpics.Models.Main main = new Models.Main();
		static NSFWpics.Models.Tools tools = new Models.Tools();
        List<NSFWpics.Models.Image> list = new List<NSFWpics.Models.Image>();

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.Id'
        public int Id { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.Id'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.PageIncrement'
        public int PageIncrement { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.PageIncrement'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.MaxId'
		public int MaxId { get; set; } = tools.MaxId(0) / 10;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.MaxId'

        [BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.Image'
        public NSFWpics.Models.Image Image { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.Image'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.OnGet(int)'
		public IActionResult OnGet(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.OnGet(int)'
		{
			if (id == 1)
			{
				return Redirect("/");
			}
			Id = id; 
			return Page();
		}
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.List()'
		public List<NSFWpics.Models.Image> List()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IndexModel.List()'
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
