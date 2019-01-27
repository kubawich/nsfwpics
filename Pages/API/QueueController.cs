using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class QueueController : Controller
    {
		readonly List<NSFWpics.Models.Image> imgList
			= new List<NSFWpics.Models.Image>();
		readonly NSFWpics.Models.Image image
			= new NSFWpics.Models.Image();

		[Produces("application/json")]
		[HttpGet("{id:int=1}")]
		public IEnumerable<List<NSFWpics.Models.Image>> Get(int id)
		{
			yield return new NSFWpics.Models.Queue().GetAll(id, imgList);
		}
	}
}
