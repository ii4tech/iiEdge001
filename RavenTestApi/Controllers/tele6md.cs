using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RavenTestApi.DbClients;
using RavenTestApi.Entities;
using RavenTestApi.Entities.Queries;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavenTestApi.Controllers
{
    [Route("api/tele6md")]
    [ApiController]
    public class tele6md : ControllerBase
    {
        // GET: api/<tele6md>
        [HttpGet]
        public string Get()
        {
            RavenDb rdb = new RavenDb();
            var log = rdb.GetAllAccels();
            return System.Text.Json.JsonSerializer.Serialize(log);
        }

        // GET api/<tele6md>/5
        [HttpGet("{edgeId}")]
        public string Get([FromRoute] string edgeId)
        {            
            
            JArray log = QryTblRawAccel.GetAccelById(edgeId);
            return JsonConvert.SerializeObject(log);
        }

        // POST api/<tele6md>
        [HttpPost]
        public async Task<int>  Post([FromBody] string value)
        {
            int r = QryTblRawAccel.InsertRaven(value);
            return r;

        }

        // PUT api/<tele6md>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<tele6md>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
