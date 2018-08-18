using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class BestController : Controller
    {
        Image image = new Image();
        [HttpGet]
        public IEnumerable<string> Get()
        {
            image.Author = NSFWpics.DBEntities.DBEntity.Instance.Best(image).Author;
            image.Date = NSFWpics.DBEntities.DBEntity.Instance.Best(image).Date;
            image.Id = NSFWpics.DBEntities.DBEntity.Instance.Best(image).Id;
            image.Points = NSFWpics.DBEntities.DBEntity.Instance.Best(image).Points;
            image.Uri = NSFWpics.DBEntities.DBEntity.Instance.Best(image).Uri;
            return new string[] { image.Author.ToString(), image.Date.ToString(), image.Id.ToString(), image.Points.ToString(), image.Uri.ToString() };
        }
    }
}
