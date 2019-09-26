using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSFWpics.Models;

namespace NSFWpics.Pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BestModel'
    public class BestModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BestModel'
    {
        [BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BestModel.Image'
		public NSFWpics.Models.Image Image { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BestModel.Image'
		Best best = new Models.Best();

        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BestModel.OnGet()'
        public IActionResult OnGet()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BestModel.OnGet()'
        {
			Image = best.GetEntry(null, Image);
            return Page();
        }
    }
}