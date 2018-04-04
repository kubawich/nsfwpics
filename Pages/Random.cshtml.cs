﻿using System;
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

            cmd = new MySqlCommand($"SELECT id FROM imgs WHERE id ORDER BY id DESC LIMIT 1;", conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Id = int.Parse(reader["id"].ToString());
            }
            int MaxId = Id;
            int i = rand.Next(1, MaxId);

            conn.Close();

            cmd = new MySqlCommand($"SELECT uri, author, date, id, points FROM imgs WHERE id={i}", conn);
            conn.Open();

            MySqlDataReader reader2 = cmd.ExecuteReader();

            while (reader.Read())
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

            conn.Close();
            return Page();
        }
    }
}