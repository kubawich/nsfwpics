using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NSFWpics.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Image'
	public class Image 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Image'
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Image.Id'
        public int Id { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Image.Id'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Image.Uri'
        public string Uri { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Image.Uri'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Image.Author'
        public string Author { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Image.Author'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Image.Points'
        public int Points { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Image.Points'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Image.Date'
        public string Date { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Image.Date'
    }
}