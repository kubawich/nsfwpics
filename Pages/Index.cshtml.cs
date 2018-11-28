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
        readonly DBEntities.DBEntity _DB = new DBEntities.DBEntity();
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
			return Page();
		}
		public List<Image> List()
		{
			if (Request.Cookies["viewType"] == "images")
			{
				list =  DBEntities.DBEntity.Instance.SiteImgsOnly(Id, list);
				return list;
			}
			else if(Request.Cookies["viewType"] == "videos")
			{
				list =  DBEntities.DBEntity.Instance.SiteVideosOnly(Id, list);
				return list;
			}
			else
			{
				list = DBEntities.DBEntity.Instance.Site(Id, list);
				return list;
			}
		}
			

       /* public List<Image> List()
        {
            MySqlConnection conn = new MySqlConnection(_DB.connection.ToString());
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
            }*/
    }
}
