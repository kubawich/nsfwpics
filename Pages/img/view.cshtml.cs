using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages.NewFolder
{
    public class viewModel : PageModel
    {
		NSFWpics.Models.View view = new Models.View();
		[BindProperty]
        public NSFWpics.Models.Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet(int id)
        {
            if (id == 0) return Redirect("/");
            else Image = view.GetEntry(id, Image);
            return Page();
        }
    }
}