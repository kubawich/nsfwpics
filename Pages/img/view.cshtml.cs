using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages.NewFolder
{
    public class viewModel : PageModel
    {        
        MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();
        public string Con { get; set; }
        public int Id { get; set; }
        [BindProperty]
        public Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet(int id)
        {
            Id = id;
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            if (id == 0)
            {
                return Redirect("/");
            }else cmd = new MySqlCommand($"SELECT uri, author, date, id, points FROM imgs WHERE id={Id}", conn);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Image = new Image
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Uri = reader["uri"].ToString(),
                    Author = reader["author"].ToString(),
                    Points = int.Parse(reader["points"].ToString()),
                    Date = reader["date"].ToString()
                };
            }

            conn.Close();
            return Page();
        }
    }
}