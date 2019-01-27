using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSFWpics.Models;

namespace NSFWpics.Interfaces
{
	interface IQueue
	{
		List<Image> GetAll(int _id, List<Image> _images);
		List<Image> GetVideos(int _id, List<Image> _images);
		List<Image> GetImages(int _id, List<Image> _images);
		void Remove(int _id, string _extension);
	}
}
