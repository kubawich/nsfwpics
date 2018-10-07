using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	/// <summary>
	/// API endpoint route for all actions connected to users in system i.e show/update/login/register/delete
	/// </summary>
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
		[HttpPost, Produces("application/json"), Route("/api/users/login")]
		public IActionResult Post([FromForm]string login, [FromForm]string password)
		{
			if (DBEntities.DBEntity.Instance.Login(login,password) == "Login success")
			{
				Response.Cookies.Append("user_loged_in", "true");
				Response.Cookies.Append("login", login);
				Response.Cookies.Append("ip", Response.HttpContext.Connection.RemoteIpAddress.ToString());
				return Redirect("https://nsfwpics.pw/Login?loged=true");
			} else return Redirect("https://nsfwpics.pw/Login?loged=false");
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
			if (DBEntities.DBEntity.Instance.Register(login, password, mail) == $"Successfully Registered user {login}")
			{
				return Redirect("https://nsfwpics.pw/login?reg=true");
			} else return Redirect("https://nsfwpics.pw/login?reg=false");
		}

		/// <summary>
		/// Updates user's params in DB (supports only string params atm.)
		/// </summary>
		/// <param name="uid">User to update</param>
		/// <param name="param">name of parameter to change i.e. login/password/mail/uid/guid...</param>
		/// <param name="value">New value for param field</param>
		/// <returns>JSON with action status</returns>
		[Produces("application/json")]
		[HttpPut("{uid}")]
		public JsonResult Put(int uid, [FromQuery]string param,[FromQuery]string value)
		{
			return Json(DBEntities.DBEntity.Instance.UpdateUser(uid, param, value));
		}

		/// <summary>
		/// Deletes user with given uid
		/// </summary>
		/// <param name="uid">User's id to delete</param>
		/// <returns>JSON result of delete action</returns>
		[Produces("application/json")]
		[HttpDelete("{uid}")]
		public JsonResult Delete(int uid)
		{
			return Json(DBEntities.DBEntity.Instance.DeleteUser(uid));
		}
	}
}
