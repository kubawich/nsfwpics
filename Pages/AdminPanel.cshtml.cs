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
        public bool IsLogedIn { get; set; } = false;
        [Required, StringLength(100)]
        public string Password { get; set; } 

        [HttpPost]
        public IActionResult OnPostLogin()
        {
            if (Request.Form["Login"] == "kubawich")
            {
                IsLogedIn = true;
         
                Response.Cookies.Append("LogedInCookie", "logedIn", new Microsoft.AspNetCore.Http.CookieOptions());
                
                return Redirect("/2");
            }

            return Redirect("/Add");
        }
    }
}