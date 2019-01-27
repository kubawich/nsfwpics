using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSFWpics.Models;

namespace NSFWpics.Interfaces
{
	interface ISingleImage
	{
		Image GetEntry(int? _id, Image _image);
	}
}
