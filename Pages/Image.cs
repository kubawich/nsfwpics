using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NSFWpics.Pages
{
	public class Image 
	{
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}