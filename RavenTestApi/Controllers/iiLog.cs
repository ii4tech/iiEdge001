using Microsoft.AspNetCore.Mvc;
using RavenTestApi.DbClients;
using Serilog;
using Newtonsoft.Json.Linq;
using RavenTestApi.Entities;
using RavenTestApi.Entities.Queries;
using JsonSerializer = System.Text.Json.JsonSerializer;
using RavenTestApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavenTestApi.Controllers
{
    [Route("iilog")]
    [ApiController]
    public class iiLog : ControllerBase
    {
        // GET: <iiLog>
        [HttpGet]
        public JArray Get()
        {
            RavenDb db = new RavenDb();
            var log = db.GetAllLogs();
            return JArray.FromObject(log);
        }

        // GET <qvxLog>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST <qvxLog>
        [HttpPost]
        public async Task<int> Post([FromBody] string value)
        {
            
            Log.Information($"qvxLog Post Body: {value}");

            return QryIiTblLogs.InsertRaven(value);

        }

        // PUT <qvxLog>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE <qvxLog>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
