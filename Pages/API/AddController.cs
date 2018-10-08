using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NSFWpics.DBEntities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NSFWpics.Pages.API
{
	/// <summary>
	/// API endpoint used to upload image/webm/gif to server and index it to db
	/// </summary>
    //No header!! Body as form-data, without key and desc, only value as file
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
		public IActionResult Post()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file == null || file.Length == 0)
                    return Content("Select file to upload");
            
                DBEntity.Instance.UploadImgToDb(DBEntity.Instance.MaxId() + 1, file, "API user");
                return Json($"Upload Successful to cdn.nsfwpics.pw/img/{DBEntity.Instance.MaxId() }{Path.GetExtension(file.FileName)}");
            }
            catch(Exception e)
            {
                return Json("Upload Failed: " + e.Message);
            }
        }
    }
}
