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
            image.Author = NSFWpics.DBEntities.DBEntity.Instance.View(id, image).Author;
            image.Date = NSFWpics.DBEntities.DBEntity.Instance.View(id, image).Date;
            image.Id = NSFWpics.DBEntities.DBEntity.Instance.View(id, image).Id;
            image.Points = NSFWpics.DBEntities.DBEntity.Instance.View(id, image).Points;
            image.Uri = NSFWpics.DBEntities.DBEntity.Instance.View(id, image).Uri;
            return new string[] { image.Author.ToString(), image.Date.ToString(), image.Id.ToString(), image.Points.ToString(), image.Uri.ToString() };
        }
    }
}
