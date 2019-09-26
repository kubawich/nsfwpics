using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueImagesController'
	public class QueueImagesController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueImagesController'
	{
		readonly List<NSFWpics.Models.Image> imgList
			= new List<NSFWpics.Models.Image>();
		readonly NSFWpics.Models.Image image 
			= new NSFWpics.Models.Image();

		[Produces("application/json")]
		[HttpGet("{id:int=1}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'QueueImagesController.Get(int)'
		public IEnumerable<List<NSFWpics.Models.Image>> Get(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'QueueImagesController.Get(int)'
		{
			yield return new NSFWpics.Models.Queue().GetImages(id, imgList);
		}
	}
}
