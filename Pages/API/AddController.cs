using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSFWpics.Pages.API
{
    [Route("api/[controller]")]
    public class AddController : Controller
    {
        [HttpPost("{value}")]
        public string Post([FromBody]string value)
        {
            return value.ToLower();
        }
    }
}
