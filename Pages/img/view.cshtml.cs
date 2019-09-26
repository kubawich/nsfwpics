using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages.NewFolder
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'viewModel'
    public class viewModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'viewModel'
    {
		NSFWpics.Models.View view = new Models.View();
		[BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'viewModel.Image'
        public NSFWpics.Models.Image Image { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'viewModel.Image'

        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'viewModel.OnGet(int)'
        public IActionResult OnGet(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'viewModel.OnGet(int)'
        {
            if (id == 0) return Redirect("/");
            else Image = view.GetEntry(id, Image);
            return Page();
        }
    }
}