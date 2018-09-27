using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
	public class User : Controller
	{
		//Get all users
		[Produces("application/json")]
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		//Get user
		[Produces("application/json")]
		[HttpGet("{id}")]
		public string Get(int uid)
		{
			return $"value {uid}";
		}

		//Login
		[Produces("application/json")]
		[ActionName("login")]
		[HttpPost("/api/[controller]/login")]
		public JsonResult Post([FromForm]string login, [FromForm]string password)
		{
			return Json("Loged");
		}

		//Register
		[Produces("application/json")]
		[ActionName("register")]
		[HttpPost("/api/[controller]/register")]
		public JsonResult Post([FromForm]string login, [FromForm]string password, [FromForm]string mail)
		{
			return Json("Registred");
		}

		//Update
		[Produces("application/json")]
		[HttpPut("{id}")]
		public void Put(int uid, [FromQuery]string param,[FromQuery]string value)
		{
		}

		//Delete
		[Produces("application/json")]
		[HttpDelete("{id}")]
		public void Delete(int uid)
		{
		}
	}
}
