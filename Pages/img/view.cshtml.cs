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
            }else cmd = new MySqlCommand($"SELECT uri from imgs where id={id}", conn);
            conn.Open();
            Con = cmd.ExecuteScalar().ToString();
            
            conn.Close();
            return Page();
        }
    }
}