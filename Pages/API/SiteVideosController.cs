using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSFWpics.Pages.API
{
	[Route("api/[controller]")]
	public class SiteVideosController : Controller
	{
		List<Image> imgList = new List<Image>();
		Image image = new Image();
		[Produces("application/json")]
		[HttpGet("{id:int=1}")]
		public IEnumerable<List<Image>> Get(int id)
		{
			yield return DBEntities.DBEntity.Instance.SiteVideosOnly(id, imgList);
		}
	}
}
