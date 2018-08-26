using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    public class RandomModel : PageModel
    {
        [BindProperty]
        public Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet()
        {
            Image = NSFWpics.DBEntities.DBEntity.Instance.Random(Image);
            return Page();
        }
    }
}