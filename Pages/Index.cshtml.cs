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
        public int MaxId { get; set; }

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


            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;


            MySqlConnection conn2 = new MySqlConnection(connection.ToString());
            MySqlCommand cmd2;

            cmd2 = new MySqlCommand($"SELECT COUNT(*) FROM imgs;", conn2);

            conn2.Open();
            MySqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                MaxId = Convert.ToInt16(int.Parse(reader2["COUNT(*)"].ToString()));
            }

            conn2.Close();


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
                    cmd = new MySqlCommand($"SELECT * FROM imgs WHERE id ORDER BY id DESC LIMIT 10;", conn);

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
                    cmd = new MySqlCommand($"SELECT * FROM imgs WHERE id ORDER BY id DESC LIMIT 10 OFFSET {Id*10-10};", conn);

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
                throw;
            }
        }

        public void Plus(string id)
        {
            int ID = Convert.ToUInt16(id);
            connection.Server = "185.28.102.194";
            connection.UserID = "root";
            connection.Password = "Kubawich1";
            connection.Database = "content";
            connection.SslMode = MySqlSslMode.None;

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;


            cmd = new MySqlCommand($"UPDATE imgs SET points = points + 1 WHERE id = {ID};", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
