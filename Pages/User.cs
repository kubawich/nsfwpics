using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NSFWpics.Pages
{
	public class User 
	{
        public string Uid { get; set; }
        public string Guid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
		public int Uploaded { get; set; }
		public string[] Uploads { get; set; }

	}
}