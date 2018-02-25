using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
    public class AddModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            return Content($"Numer {id}");
        }
    }
}