using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages
{
    /// <summary>
    /// Returns JSON with /random author, date, id, points count and unique uri
    /// </summary>
    [Route("api/[controller]")]
    public class RandomController : Controller
    {
        Image image = new Image();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                image.Author = NSFWpics.DBEntities.DBEntity.Instance.Random(image).Author;
                image.Date = NSFWpics.DBEntities.DBEntity.Instance.Random(image).Date;
                image.Id = NSFWpics.DBEntities.DBEntity.Instance.Random(image).Id;
                image.Points = NSFWpics.DBEntities.DBEntity.Instance.Random(image).Points;
                image.Uri = NSFWpics.DBEntities.DBEntity.Instance.Random(image).Uri;
            
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
