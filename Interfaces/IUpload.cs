using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NSFWpics.Interfaces
{
	interface IUpload
	{
		void Upload(ITools _maxIdPlusOne, IFormFile _file, IUser _login);
	}
}
