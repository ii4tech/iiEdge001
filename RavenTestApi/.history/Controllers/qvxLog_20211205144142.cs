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
    [Route("qvxlog")]
    [ApiController]
    public class qvxLog : ControllerBase
    {
        // GET: <qvxLog>
        [HttpGet]
        public async Task<JObject> Get()
        {
            IGenericRepository gr = new SqliteRepository();
            TblRawAccel accel = new TblRawAccel();

            accel.Time = Util.getEpoch(DateTime.UtcNow);
            accel.X = 453;
            accel.Y = 556;
            accel.Z = 489;

            QryTblRawAccel qryAccel = new QryTblRawAccel(accel);
            JObject json = JObject.FromObject(accel);

            var addrecord = gr.AddAsync(qryAccel.Insert());
            Log.Information($"Add initial record result  = {addrecord.Result}");

            return json;
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
