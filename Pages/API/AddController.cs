using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
    [Route("api/[controller]")]
    public class AddController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

             DBEntities.DBEntity.Instance.UploadImgToDb(DBEntities.DBEntity.Instance.MaxId() + 1, file);
            return Content("ok");
        }
    }
}
