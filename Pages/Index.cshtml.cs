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
        public List<Image> list = new List<Image>();
        public int Id { get; set; }
        public int PageIncrement { get; set; }

        [BindProperty]
        public Image Image { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == 1)
            {
                return Redirect("/");
            }
            Id = id;
            PageIncrement = ((id * 10) - 9) ;
            return Page();
        }

        public List<Image> List()
        {
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            try
            {
                if (Id == 0 || Id == 1)
                {
                    cmd = new MySqlCommand($"SELECT * FROM imgs WHERE id BETWEEN {Id} AND {Id + 11}", conn);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Image
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Uri = reader["uri"].ToString(),
                            Author = reader["author"].ToString(),
                            Points = int.Parse(reader["points"].ToString()),
                            Date = reader["date"].ToString()
                        });
                    }

                    conn.Close();
                    return list;
                }
                else
                {
                    cmd = new MySqlCommand($"SELECT * FROM imgs WHERE id BETWEEN {(Id * 10 - 9)} AND {(Id * 10)}", conn);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Image
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Uri = reader["uri"].ToString(),
                            Author = reader["author"].ToString(),
                            Points = int.Parse(reader["points"].ToString()),
                            Date = reader["date"].ToString()
                        });
                    }

                    conn.Close();
                    return list;
                }
            }
            catch (Exception)
            {
                return list;
            } 
        }
    }
}
