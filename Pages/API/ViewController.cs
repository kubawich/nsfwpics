using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ViewController'
    public class ViewController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ViewController'
    {
		NSFWpics.Models.Image image = new NSFWpics.Models.Image();
		NSFWpics.Models.View view = new NSFWpics.Models.View();

		[Produces("application/json")]		
		[HttpGet("{id:int}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ViewController.Get(int)'
        public Dictionary<string, string> Get(int id)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ViewController.Get(int)'
        {
			return new Dictionary<string, string> {
				{"ID", view.GetEntry(id, image).Id.ToString() },
				{"Points", view.GetEntry(id, image).Points.ToString() },
				{"Author", view.GetEntry(id, image).Author },
				{"Date uploaded", view.GetEntry(id, image).Date },
				{"Link", view.GetEntry(id, image).Uri },
				{"Max site id", new Models.Tools().MaxId(0).ToString()},
				{"Dir", Directory.GetCurrentDirectory()},
				{"Max queue id", new Models.Tools().MaxId(3).ToString()}
			};
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
				new NSFWpics.Models.Main().Remove(id, extension);
				return Json($"Removed main site entity at id: {id}");
			}
			else if (type == 1)
			{
				new NSFWpics.Models.Queue().Remove(id, extension);
				return Json($"Removed queue entity at id: {id}");
			}
			else return null;
        }
    }
}
