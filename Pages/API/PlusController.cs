using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class PlusController : Controller
    {
		[Produces("application/json")]
		[HttpGet("{id:int}")]
        public JsonResult Get(int id)
        {
			new NSFWpics.Models.Tools().Plus(id);
			return Json($"points updated +1 on {id}");
        }
    }
}
