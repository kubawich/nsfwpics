﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SiteVideosController'
	public class SiteVideosController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SiteVideosController'
	{
		List<NSFWpics.Models.Image> imgList = new List<NSFWpics.Models.Image>();
		NSFWpics.Models.Image image = new NSFWpics.Models.Image();

		[Produces("application/json")]
		[HttpGet("{id:int=1}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SiteVideosController.Get(int)'
		public IEnumerable<List<NSFWpics.Models.Image>> Get(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SiteVideosController.Get(int)'
		{
			yield return new NSFWpics.Models.Main().GetVideos(id, imgList);
		}
	}
}
