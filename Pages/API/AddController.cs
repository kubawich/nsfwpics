using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore;

namespace NSFWpics.Pages.API
{
    /// <summary>
    /// API endpoint used to upload image/webm/gif to server and index it to db
    /// </summary>
    ///No header!! Body as form-data, without key and desc, only value as file
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddController : Controller
    {
        [BindProperty]
        IFormFile file { get; set; }

        [HttpPost]
        [ProducesResponseType(200)]
        [RequireHttps]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'AddController.Post()'
		public IActionResult Post()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'AddController.Post()'
        {
            try
            {
                file = Request.Form.Files[0];
				
                if (file == null || file.Length == 0)
                    return Content("Select file to upload");

				new Models.Upload().UploadToQueue((new Models.Tools().MaxId(3) + 1), file, "API");
				
                return Json($"Upload Successful to cdn.nsfwpics.pw/img_queue/{new Models.Tools().MaxId(3)}{Path.GetExtension(file.FileName)}");
            }
            catch(Exception e)
            {
                return Json("Upload Failed: " + e.Message);
            }
        }
    }
}
