using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'PlusController'
    public class PlusController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'PlusController'
    {
		[Produces("application/json")]
		[HttpGet("{id:int}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'PlusController.Get(int)'
        public JsonResult Get(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'PlusController.Get(int)'
        {
			new NSFWpics.Models.Tools().Plus(id);
			return Json($"points updated +1 on {id}");
        }
    }
}
