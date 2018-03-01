using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NSFWpics.Pages
{
    public class AdminPanelModel : PageModel
    {
        public string Login { get; set; } = "Login";
        [Required, StringLength(100)]
        public string Password { get; set; } 

        [HttpPost]
        public IActionResult OnPostLogin()
        {
            if (Request.Form["Login"] == "kubawich")
            {
                
                /*Response.Cookies["AdminLogin"][""]
                return Redirect("/2");*/
            }

            return Redirect("/Add");
        }
    }
}