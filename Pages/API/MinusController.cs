using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'MinusController'
    public class MinusController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'MinusController'
    {
        [HttpGet("{id:int}")]
		[Produces("application/json")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'MinusController.Get(int)'
		public string Get(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'MinusController.Get(int)'
        {
            new Models.Tools().Minus(id);
            return $"points updated -1 on {id}";
        }
    }
}
