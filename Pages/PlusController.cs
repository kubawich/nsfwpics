using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class PlusController : Controller
    {
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            NSFWpics.DBEntities.DBEntity.Instance.Plus(id);
            return $"points updated +1 on {id}";
        }
    }
}
