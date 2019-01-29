using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
		Models.Upload upload = new Models.Upload();

		[BindProperty]
		IFormFile file { get; set; }

		[HttpPost]
		public IActionResult Post()
        {
            try
            {
                file = Request.Form.Files[0];
				
                if (file == null || file.Length == 0)
                    return Content("Select file to upload");

				upload.UploadToQueue((new Models.Tools().MaxId(3) + 1), file, "API");
				
                return Json($"Upload Successful to cdn.nsfwpics.pw/img_queue/{new Models.Tools().MaxId(3)}{Path.GetExtension(file.FileName)}");
            }
            catch(Exception e)
            {
                return Json("Upload Failed: " + e.Message);
            }
        }
    }
}
