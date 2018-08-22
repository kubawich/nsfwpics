using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Renci.SshNet;

namespace NSFWpics.Pages
{
    public class AddModel : PageModel
    {
        MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();
        [BindProperty]
        public IFormFile Upload { get; set; }
        [BindProperty]
        public Image Image { get; set; }
        [BindProperty]
        public string UploadOption { get; set; }
        public int Id { get; set; }
        private IHostingEnvironment _environment;

        public AddModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [RequestSizeLimit(40000000)]
        public async Task<IActionResult> OnPostAsync(IFormFile files)
        {
            var filePath = string.Empty;
            if (_environment.IsDevelopment())
            {
                filePath = Path.GetTempPath();
            }
            else
            {
                filePath = "/var/www/html/img";
            }

            var MaxId = DBEntities.DBEntity.Instance.MaxId() + 1;

            files = Upload;
            var extension = Path.GetExtension(files.FileName);
            using (SftpClient client = new SftpClient("185.28.102.194", 22, "root", "Kubawich1"))
            {
                if (files != null)
                {
                    client.Connect();
                    client.ChangeDirectory("/var/www/html/img");
                    using (FileStream fs = new FileStream(Path.GetFileName(Upload.FileName), FileMode.Create))
                    {
                          client.UploadFile(Upload.OpenReadStream(), 
                              $"{MaxId}{Path.GetExtension(files.FileName)}");
                        //await files.CopyToAsync(stream);
                    }

                }
                client.Disconnect();
                client.Dispose();
            }
        DBEntities.DBEntity.Instance.InsertImgToDb(MaxId, extension.ToString());


            return Redirect("/Add");
        }




        private async Task<CaptchaVerification> VerifyCaptcha()
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null) userIP = ipAddress.MapToIPv4().ToString();

            var captchaResponse = Request.Form["g-recaptcha-response"];
            var payload = string.Format("&secret={0}&remoteip={1}&response={2}",
                "6LeWRUoUAAAAAGDSGfW16HjwDjFTED_blkHccZhD",
                userIP,
                captchaResponse
                );

            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");
            var request = new HttpRequestMessage(HttpMethod.Post, "/recaptcha/api/siteverify");
            request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<CaptchaVerification>(response.Content.ReadAsStringAsync().Result);
        }
    }

    public class CaptchaVerification
    {
        public CaptchaVerification()
        {
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public IList Errors { get; set; }
    }
}