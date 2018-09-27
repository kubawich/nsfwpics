using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class RandomController : Controller
    {
        Image image = new Image();
		[Produces("application/json")]
		[HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
				image = DBEntities.DBEntity.Instance.Random(image);            
                return new string[] { image.Author.ToString(), image.Date.ToString(), image.Id.ToString(), image.Points.ToString(), image.Uri.ToString() };
            }
            catch (Exception)
            {
                Get();
                return new string[] {image.Author.ToString(), image.Date.ToString(), image.Id.ToString(), image.Points.ToString(), image.Uri.ToString(), "No entry, returning other"};
            }
        }
    }
}
