using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages
{
    public class AdminPanelController : Controller
    {
        [HttpGet]
        public IActionResult GRR()
        {
            return Content($"Hello ");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(AdminPanelModel model)
        {
            return Content($"Hello {model.Login} {model.Password}");
        }
    }
}