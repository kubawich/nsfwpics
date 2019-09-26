using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSFWpics.Models;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RandomController'
    public class RandomController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RandomController'
    {
		NSFWpics.Models.Image image = new NSFWpics.Models.Image();
		NSFWpics.Models.Random random = new NSFWpics.Models.Random();

		[Produces("application/json")]
		[HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RandomController.Get()'
        public Dictionary<string, string> Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RandomController.Get()'
        {
            try
            {
				image = random.GetEntry(null, image);            
                return new Dictionary<string, string>
				{
					{"ID", image.Id.ToString() },
					{"Points", image.Points.ToString() },
					{"Author", image.Author },
					{"Date uploaded", image.Date },
					{"Link", image.Uri }
				};
            }
            catch (Exception)
            {
                Get();
				return new Dictionary<string, string>
				{
					{"ID", image.Id.ToString() },
					{"Points", image.Points.ToString() },
					{"Author", image.Author },
					{"Date uploaded", image.Date },
					{"Link", image.Uri }
				};
			}
        }
    }
}
