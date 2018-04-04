using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    public class RandomModel : PageModel
    {
        Random rand = new Random();

        public MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();
        public int Id { get; private set; }
        public string Con { get; private set; }
        [BindProperty]
        public Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet()
        {

            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            cmd = new MySqlCommand($"SELECT COUNT(*) FROM imgs;", conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Id = int.Parse(reader["COUNT(*)"].ToString());
            }
            int MaxId = Id;
            int i = rand.Next(1, 10700);

            conn.Close();

            MySqlConnection conn2 = new MySqlConnection(connection.ToString());
            MySqlCommand cmd2 = new MySqlCommand($"SELECT uri, author, date, id, points FROM imgs WHERE id={i}", conn2);
            conn2.Open();

            MySqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
               Image = new Image
                {
                    Id = int.Parse(reader2["id"].ToString()),
                    Uri = reader2["uri"].ToString(),
                    Author = reader2["author"].ToString(),
                    Points = int.Parse(reader2["points"].ToString()),
                    Date = reader2["date"].ToString()
                };
            }

            conn2.Close();
            return Page();
        }
    }
}