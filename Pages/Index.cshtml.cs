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
				MaxId = _DB.MaxId(1) / 10;
				return list;
			}
			else if(Request.Cookies["viewType"] == "videos")
			{
				list =  DBEntities.DBEntity.Instance.SiteVideosOnly(Id, list);
				MaxId = _DB.MaxId(2) / 10;
				return list;
			}
			else
			{
				list = DBEntities.DBEntity.Instance.Site(Id, list);
				MaxId = _DB.MaxId(0) / 10;
				return list;
			}
		}
    }
}
