using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace NSFWpics.Pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel'
    public class QueueModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel'
    {
		NSFWpics.Models.Queue queue = new Models.Queue();
		static NSFWpics.Models.Tools tools = new Models.Tools();
		List<NSFWpics.Models.Image> list = new List<NSFWpics.Models.Image>();

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.Id'
		public int Id { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.Id'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.PageIncrement'
        public int PageIncrement { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.PageIncrement'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.MaxId'
		public int MaxId { get; set; } = tools.MaxId(3) / 10;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.MaxId'

        [BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.Image'
        public NSFWpics.Models.Image Image { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.Image'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.OnGet(int)'
		public IActionResult OnGet(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.OnGet(int)'
		{
			if (id == 1)
			{
				return Redirect("/queue");
			}
			Id = id;
			return Page();
		}
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.List()'
		public List<NSFWpics.Models.Image> List()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueModel.List()'
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
