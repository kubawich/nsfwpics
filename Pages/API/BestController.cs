using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSFWpics.Models;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BestController'
    public class BestController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BestController'
    {
		NSFWpics.Models.Image image = new NSFWpics.Models.Image();
		Best best = new Best();

		[Produces("application/json")]
		[HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BestController.Get()'
        public Dictionary<string, string> Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BestController.Get()'
        {
			return new Dictionary<string, string> {
				{"ID", best.GetEntry(null, image).Id.ToString() },
				{"Points", best.GetEntry(null, image).Points.ToString() },
				{"Author", best.GetEntry(null, image).Author },
				{"Date uploaded", best.GetEntry(null, image).Date },
				{"Link", best.GetEntry(null, image).Uri }
			};
		}
    }
}
