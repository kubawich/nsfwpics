using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSFWpics.Models;

namespace NSFWpics.Pages
{
    public class BestModel : PageModel
    {
        [BindProperty]
		public NSFWpics.Models.Image Image { get; set; }
		Best best = new Models.Best();

        [HttpGet]
        public IActionResult OnGet()
        {
			Image = best.GetEntry(null, Image);
            return Page();
        }
    }
}