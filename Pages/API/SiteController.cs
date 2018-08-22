using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class SiteController : Controller
    {
        List<Image> imgList = new List<Image>();
        Image image = new Image();
        [HttpGet("{id}")]
        public IEnumerable<List<Image>> Get(int id)
        {
            yield return DBEntities.DBEntity.Instance.Site(id, imgList);
        }
    }
}
