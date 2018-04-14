﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace NSFWpics.Pages.aditional
{
    public class contactModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var resultCaptcha = await VerifyCaptcha();
            if (!resultCaptcha.Success)
            {
                ModelState.AddModelError("", "Captcha is not valid");
                return Redirect("/contact");
            }
            if (ModelState.IsValid)
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("kubawich45@gmail.com");
                mail.To.Add("jakub.wichlinski@yahoo.com");
                mail.Subject = "Test Mail - 1";
                mail.Body = "mail with attachment";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("kubawich45@gmail.com", "Kubawich1");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return Redirect("/contact");
            }
            else return Redirect("/contact");
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
}