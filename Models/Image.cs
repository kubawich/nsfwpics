using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NSFWpics.Models
{
	public class Image 
	{
        public int Id { get; set; }
        public string Uri { get; set; }
        public string Author { get; set; }
        public int Points { get; set; }
        public string Date { get; set; }
    }
}