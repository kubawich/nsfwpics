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
        public string Con { get; set; }
        public int Id { get; set; }
        [BindProperty]
        public Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet(int id)
        {
            if (id == 0) return Redirect("/");
            else Image = NSFWpics.DBEntities.DBEntity.Instance.View(id, Image);
            return Page();
        }
    }
}