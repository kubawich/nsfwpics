using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSFWpics.DBEntities;

namespace NSFWpics.Pages.API
{
    //No header!! Body as form-data, without key and desc, only value as file
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file == null || file.Length == 0)
                    return Content("Select file to upload");
            
                DBEntity.Instance.UploadImgToDb(DBEntity.Instance.MaxId() + 1, file);
                return Json($"Upload Successful to cdn.nsfwpics.pw/img/{DBEntity.Instance.MaxId() + 1}{Path.GetExtension(file.FileName)}");
            }
            catch(Exception e)
            {
                return Json("Upload Failed: " + e.Message);
            }
        }
    }
}
