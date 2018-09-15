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
		/// <param name="extension">Content's file extension in ?extension=png/jpeg/webm... format </param>
		/// <returns>Json with sucedeed status</returns>
        [HttpDelete("{id}")]
        public JsonResult Delete([FromRoute]int id, [FromQuery] string extension)
        {
            DBEntities.DBEntity.Instance.RemoveImg(id, extension);
			return Json($"Removed entity at id: {id}");
        }
    }
}
