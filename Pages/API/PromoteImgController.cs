﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
	public class PromoteImgController : Controller
	{
		[Produces("application/json")]
		[HttpPost("{_id:int?}")]
		public JsonResult Post([FromRoute]int? _id)
		{
			new NSFWpics.Models.Tools().PromoteImage(_id);
			return new JsonResult($"{_id} promoted sucessfuly");
		}
	}
}
