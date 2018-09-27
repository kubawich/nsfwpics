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
		[ActionName("Login")]
		[Produces("application/json")]
		[HttpPost]
		public IActionResult Post([FromBody]string login, [FromBody]string password)
		{
			return Ok();
		}

		//Register
		[ActionName("Register")]
		[Produces("application/json")]
		[HttpPost]
		public IActionResult Post([FromBody]string login, [FromBody]string password, [FromBody]string mail)
		{
			return Created("dd", null);
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
