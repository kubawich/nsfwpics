using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    public class IndexController : Controller
    {
        MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();
        public string cc,rows;
        [HttpGet("Index")]
        public IActionResult Index()
        {
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            for (int i = 1; i < 120 ; i++)
            {
                MySqlCommand cmd = new MySqlCommand(String.Format($"INSERT INTO imgs(uri,author,points) values('http://185.28.102.194/img/{i}.png','Lerp',0)"), conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return View();
        }
    }
}