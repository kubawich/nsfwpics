using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NSFWpics.Interfaces
{
	interface IUploads
	{
		int UploadToMain(int _maxIdPlusOne, IFormFile _file, string _login);
		int UploadToQueue(int _maxIdPlusOne, IFormFile _file, string _login);
	}
}
