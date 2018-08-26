using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace NSFWpics.Pages
{
    [Route("api/[controller]")]
    public class PlusController : Controller
    {
        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            NSFWpics.DBEntities.DBEntity.Instance.Plus(id);
            return $"points updated +1 on {id}";
        }
    }
}
