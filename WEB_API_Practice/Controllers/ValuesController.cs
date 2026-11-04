using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Web.Http;

namespace WEB_API_Practice.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        List<string> str = new List<string> { "Komal", "anubhav", "avijit" };
        public IEnumerable<string> Get()
        {
            return str;
        }

        // GET api/values/5
        [HttpGet]
        public string retreiveByID([FromUri] int id)
        {
            string getval = str[id];
            return getval;
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] string value)
        {
            
            str.Add(value);
            return Ok(str.ToList());
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            str[id] = value;
            return Ok(str.ToList());
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            string delval = str[id];
            str.RemoveAt(id);
            return Ok($"Deleted User {delval} successfully");
        }
    }
}
