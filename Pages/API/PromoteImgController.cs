using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'PromoteImgController'
	public class PromoteImgController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'PromoteImgController'
	{
		[Produces("application/json")]
		[HttpPost("{_id:int?}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'PromoteImgController.Post(int?)'
		public JsonResult Post([FromRoute]int? _id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'PromoteImgController.Post(int?)'
		{
			new NSFWpics.Models.Tools().PromoteImage(_id);
			return new JsonResult($"{_id} promoted sucessfuly");
		}
	}
}
