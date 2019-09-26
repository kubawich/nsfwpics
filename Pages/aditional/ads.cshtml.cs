using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace NSFWpics.Pages.aditional
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'adsModel'
    public class adsModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'adsModel'
    {
        MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'adsModel.OnGet()'
        public void OnGet()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'adsModel.OnGet()'
        {
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'adsModel.OnPostAsync()'
        public async Task<IActionResult> OnPostAsync()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'adsModel.OnPostAsync()'
        {
            var resultCaptcha = await VerifyCaptcha();
            if (!resultCaptcha.Success)
            {
                ModelState.AddModelError("", "Captcha is not valid");
                return Redirect("/ads");
            }
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            cmd = new MySqlCommand($"INSERT INTO messages(name,site,phone,content) values('{Request.Form["name"]}','{Request.Form["web"]}','{Request.Form["phone"]}','{Request.Form["content"]}','{Request.Form["mail"]}')", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return Redirect("http://nsfwpics.pw/aditional/ads");
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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification'
    public class CaptchaVerification
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification.CaptchaVerification()'
        public CaptchaVerification()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification.CaptchaVerification()'
        {
        }

        [JsonProperty("success")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification.Success'
        public bool Success { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification.Success'

        [JsonProperty("error-codes")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification.Errors'
        public IList Errors { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'CaptchaVerification.Errors'
    }
}