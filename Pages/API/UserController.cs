using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		//Get all users
		[Produces("application/json")]
		[HttpGet]
		public IEnumerable<User> Get()
		{
			yield return new User();
		}

		//Get user
		[Produces("application/json")]
		[HttpGet("{id}")]
		public User Get(int uid)
		{
			return new User();
		}



		//Register
		[Produces("application/json")]
		[HttpPost]
		public JsonResult Post([FromForm]string login, [FromForm]string password, [FromForm]string mail)
		{
			var i = DBEntities.DBEntity.Instance.Register(login, password, mail);
			return Json((i == 1) ? "User already exists" : "Successfully Registered user");
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
