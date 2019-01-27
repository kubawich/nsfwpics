using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSFWpics.Interfaces
{
	interface ITools
	{
		void Plus(int _id);
		void Minus(int _id);
		int MaxId(int _siteType);
		void PromoteImage(int? _id);
	}
}
