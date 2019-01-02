using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class ViewController : Controller
    {
        Image image = new Image();
		[Produces("application/json")]
		[HttpGet("{id:int}")]
        public IEnumerable<string> Get(int id)
        {
            image.Author = DBEntities.DBEntity.Instance.View(id, image).Author;
            image.Date = DBEntities.DBEntity.Instance.View(id, image).Date;
            image.Id = DBEntities.DBEntity.Instance.View(id, image).Id;
            image.Points = DBEntities.DBEntity.Instance.View(id, image).Points;
            image.Uri = DBEntities.DBEntity.Instance.View(id, image).Uri;
            return new string[] { image.Author.ToString(), image.Date.ToString(), image.Id.ToString(), image.Points.ToString(), image.Uri.ToString() };
        }

		/// <summary>
		/// Removes content with given ID and extension
		/// </summary>
		/// <param name="id">ID of content entry to remove</param>
		/// <param name="type">What site it's related to? Main or queue (0/1)</param>
		/// <param name="extension">Content's file extension in ?extension=png/jpeg/webm... format </param>
		/// <returns>Json with sucedeed status</returns>
		[Produces("application/json")]
		[HttpDelete("{id}/{type}")]
        public JsonResult Delete([FromRoute]int id, [FromRoute]int type, [FromQuery] string extension)
        {
			if (type == 0)
			{
				DBEntities.DBEntity.Instance.RemoveImg(id, extension);
				return Json($"Removed main site entity at id: {id}");
			}
			else if (type == 1)
			{
				DBEntities.DBEntity.Instance.RemoveQueueImg(id, extension);
				return Json($"Removed queue entity at id: {id}");
			}
			else return null;
        }
    }
}
