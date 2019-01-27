using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class MinusController : Controller
    {
        [HttpGet("{id:int}")]
		[Produces("application/json")]
		public string Get(int id)
        {
            new NSFWpics.Models.Tools().Minus(id);
            return $"points updated -1 on {id}";
        }
    }
}
