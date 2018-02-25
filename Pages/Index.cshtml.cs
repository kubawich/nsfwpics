using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    public class IndexModel : PageModel
    {
        MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder();
        public List<string>[] list;
        public int Id { get; set; }
        public int PageIncrement { get; set; }
        [BindProperty]
        public Image Image { get; set; }

        public IActionResult OnGet(int id)
        {
            Id = id;
            PageIncrement = ((id * 10) - 10) +1;
            return Page();
        }

        public List<string>[] List()
        {
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            //Create a list to store the result
            list = new List<string>[5];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd = new MySqlCommand("SELECT * from imgs", conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list[0].Add(reader["id"].ToString() + "");
                list[1].Add(reader["uri"].ToString() + "");
                list[2].Add(reader["author"].ToString() + "");
                list[3].Add(reader["points"].ToString() + "");
                list[4].Add(reader["date"].ToString() + "");
            }
            conn.Close();
            return list;
        }
    }
}
