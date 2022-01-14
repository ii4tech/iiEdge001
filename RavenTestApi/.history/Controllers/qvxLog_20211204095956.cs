using Microsoft.AspNetCore.Mvc;
using RavenTestApi.DbClients;
using RavenTestApi.DTO;
using Serilog;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RavenTestApi.Entities;
using RavenTestApi.Entities.Queries;
using JsonSerializer = System.Text.Json.JsonSerializer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavenTestApi.Controllers
{
    [Route("qvxlog")]
    [ApiController]
    public class qvxLog : ControllerBase
    {
        // GET: <qvxLog>
        [HttpGet]
        public async Task<JArray> Get()
        {
            SqliteRepository repo = new SqliteRepository();
            
            JArray array = repo.GetAll("QiTblLogs");

            return array;
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

            QiTblLogs? qiTblLogs = JsonSerializer.Deserialize<QiTblLogs>(value);

            QryQiTblLogs qryQiTblLogs = new QryQiTblLogs(qiTblLogs);

            SqliteRepository repo = new SqliteRepository();

            return await repo.AddAsyncFromQry(qryQiTblLogs.Insert());

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
