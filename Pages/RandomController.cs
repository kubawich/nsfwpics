using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSFWpics.Pages
{
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
            catch (Exception e)
            {
                Get();
                return new string[] {image.Author.ToString(), image.Date.ToString(), image.Id.ToString(), image.Points.ToString(), image.Uri.ToString(), "No entry, returning other"};
            }
        }
    }
}
