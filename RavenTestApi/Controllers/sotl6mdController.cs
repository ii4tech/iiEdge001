using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RavenTestApi.DbClients;
using RavenTestApi.Entities;
using RavenTestApi.Entities.Queries;
using Serilog;
using System.Text.Json;

namespace RavenTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sotl6mdController : ControllerBase
    {
        // GET: /sotl6md
        [HttpGet]
        public string Get()
        {
            return "SOTL6md API";
        }

        // GET /sotl6md/5
        [HttpGet("{id}")]
        public async Task<JArray> Get(string range)
        {
            

            return new JArray();

        }

        // POST /sotl6md
        [HttpPost]
        public async Task<int> Post([FromBody] string value)
        {
            //Log.Information($"RawAccel Post Body: {value}");

            //TblRawAccel? accel = JsonSerializer.Deserialize<TblRawAccel>(value);

            //QryTblRawAccel qryAccel = new QryTblRawAccel(accel);

            //SqliteRepository repo = new SqliteRepository();

            //int resp = await repo.AddAsyncFromQry(qryAccel.Insert());

            //Log.Information($"add accel response: {resp}");

            return 0;
        }

        // DELETE /sotl6md
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
