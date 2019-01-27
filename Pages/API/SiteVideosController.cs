using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
	public class SiteVideosController : Controller
	{
		List<NSFWpics.Models.Image> imgList = new List<NSFWpics.Models.Image>();
		NSFWpics.Models.Image image = new NSFWpics.Models.Image();

		[Produces("application/json")]
		[HttpGet("{id:int=1}")]
		public IEnumerable<List<NSFWpics.Models.Image>> Get(int id)
		{
			yield return new NSFWpics.Models.Main().GetVideos(id, imgList);
		}
	}
}
