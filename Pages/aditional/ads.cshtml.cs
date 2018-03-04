using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages.aditional
{
    public class adsModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPostSend()
        {
            return Page();
        }
    }
}