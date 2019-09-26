using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages.NewFolder
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'idModel'
    public class idModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'idModel'
    {        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'idModel.Con'
        public string Con { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'idModel.Con'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'idModel.Id'
        public int Id { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'idModel.Id'

        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'idModel.OnGet(int)'
        public IActionResult OnGet(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'idModel.OnGet(int)'
        {
            return Page();
        }
    }
}