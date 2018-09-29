using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		/// <summary>
		/// Get all users from DB
		/// </summary>
		/// <returns>JSON with all users in DB</returns>
		[HttpGet, Produces("application/json")]
		public IEnumerable<List<User>> Get()
		{
			 yield return DBEntities.DBEntity.Instance.GetUser(null);
		}

		/// <summary>
		/// Show user data with given id
		/// </summary>
		/// <param name="id">User identifeir in DB</param>
		/// <returns>User data with given id</returns>
		[HttpGet("{id}"), Produces("application/json")]
		public IEnumerable<User> Get(int id)
		{
			return DBEntities.DBEntity.Instance.GetUser(id);
		}

		/// <summary>
		/// Endpoint used to Login user to system
		/// </summary>
		/// <param name="login">Login used to identify user in system</param>
		/// <param name="password">SHA1 hash to identify user's identity in DB</param>
		/// <returns></returns>
		[HttpPost, Produces("application/json"), Route("login")]
		public IActionResult Post([FromForm]string login, [FromForm]string password)
		{
			return RedirectPermanentPreserveMethod("https://nsfwpics.pw/Login?register=Success");
		}

		/// <summary>
		/// Endpoint used to Register new user to system
		/// </summary>
		/// <param name="login">Login used to identify user in system</param>
		/// <param name="password">Password to identify user in DB presented as SHA1 hash</param>
		/// <param name="mail">Email address to identify user in system</param>
		/// <returns></returns>
		[HttpPost, Produces("application/json"), Route("register")]
		public IActionResult Post([FromForm]string login, [FromForm]string password, [FromForm]string mail)
		{
			var i = DBEntities.DBEntity.Instance.Register(login, password, mail);
			return View("Login.cshtml");
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
