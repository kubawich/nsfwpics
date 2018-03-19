using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
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

        public void OnGet()
        {
        }

        public void OnPost()
        {
            #region SQL_server_connection
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            cmd = new MySqlCommand($"SELECT id FROM imgs WHERE id ORDER BY id DESC LIMIT 1;", conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Id = int.Parse(reader["id"].ToString());
            }
            int MaxId = Id;

            conn.Close();
            #endregion
            if (Request.Form["UploadOption"] == "1")
            {
                #region PHOTOS_UPLOAD
                //Upload photo to server
                string name = Upload.FileName;
                string extension = name.Substring(name.Length - 3);
                if (extension == "png" || extension == "jpg" || extension == "peg" || extension == "ebp")
                {
                    using (SftpClient client = new SftpClient("185.28.102.194", 22, "root", "Kubawich1"))
                    {
                        client.Connect();
                        client.ChangeDirectory("/var/www/html/img");

                        using (FileStream fs = new FileStream(Path.GetFileName(Upload.FileName), FileMode.Create))
                        {
                            client.BufferSize = 4 * 1024;
                            if (extension == "png" || extension == "jpg" || extension == "jpeg")
                            {
                                client.UploadFile(Upload.OpenReadStream(), $"{MaxId + 1}.png");
                            }
                            else if (extension == "ebp")
                            {
                                client.UploadFile(Upload.OpenReadStream(), $"{MaxId + 1}.webp");
                            }
                            else Redirect("/Add");
                        }
                        client.Disconnect();
                        client.Dispose();
                    }
                    //Add uploaded photo to SQL database
                    cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) values('http://cdn.nsfwpics.pw/img/{MaxId + 1}.png','Anonymous',0)", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else Redirect("/add");
                #endregion
            }
            else if (Request.Form["UploadOption"] == "2")
            {
                #region PHOTOS_videos
                //Upload photo to server
                string name = Upload.FileName;
                string extension = name.Substring(name.Length - 4);
                if (extension == ".mp4" || extension == "webm" || extension == "apng")
                {
                    using (SftpClient client = new SftpClient("185.28.102.194", 22, "root", "Kubawich1"))
                    {
                        client.Connect();
                        client.ChangeDirectory("/var/www/html/img");

                        using (FileStream fs = new FileStream(Path.GetFileName(Upload.FileName), FileMode.Create))
                        {
                            client.BufferSize = 4 * 1024;
                            if (extension == ".mp4")
                            {
                                client.UploadFile(Upload.OpenReadStream(), $"{MaxId + 1}.mp4");
                            }
                            else if (extension == "webm")
                            {
                                client.UploadFile(Upload.OpenReadStream(), $"{MaxId + 1}.webm");
                            }
                            else if (extension == "apng")
                            {
                                client.UploadFile(Upload.OpenReadStream(), $"{MaxId + 1}.apng");
                            }
                            else Redirect("/Add");
                        }
                        client.Disconnect();
                        client.Dispose();
                    }
                    //Add uploaded photo to SQL database
                    cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) values('http://cdn.nsfwpics.pw/img/{MaxId + 1}.mp4','Anonymous',0)", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else Redirect("/add");
                #endregion
            }
            else if (Request.Form["UploadOption"] == "3")
            {
            #region streamable
            //Upload streamable to server                                
            cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) values('{Request.Form["streamable_link"]}','Anonymous',0)", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            }
            else Redirect("/add");
            #endregion
    }
    }
}