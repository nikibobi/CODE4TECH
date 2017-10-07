using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CODE4TECH.Controllers
{
    using Database;

    [Route("api")]
    public class DataController : Controller
    {
        private readonly Code4TechDbContext context;

        public DataController(Code4TechDbContext context)
        {
            this.context = context;
        }

        // GET: api
        [HttpGet]
        public IEnumerable<Reading> Get()
        {
            return context.Readings;
        }

        // POST api
        [HttpPost]
        public void Post([FromBody]Reading reading)
        {
            context.Readings.Add(reading);
            context.SaveChanges();
        }
    }
}
