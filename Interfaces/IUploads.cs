using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NSFWpics.Interfaces
{
	interface IUploads
	{
		void UploadToMain(int _maxIdPlusOne, IFormFile _file, string _login);
		void UploadToQueue(int _maxIdPlusOne, IFormFile _file, string _login);
	}
}
