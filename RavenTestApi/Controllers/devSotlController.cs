using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RavenTestApi.DbClients;
using RavenTestApi.DTO;
using RavenTestApi.Entities;
using RavenTestApi.Entities.Queries;
using RavenTestApi.Models;
using Serilog;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavenTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class devSotlController : ControllerBase
    {
        RavenDb rdb;

        public devSotlController()
        {
            rdb = new RavenDb();

        }
        // GET: api/<devSotl>
        [HttpGet]
        public JObject Get()
        {
            
            sotl6mdpack data = new sotl6mdpack();
            RawAccelDTO accel = new RawAccelDTO();

            accel.x = 528;
            accel.y = 523;
            accel.z = 605;
            accel.time = DateTime.UnixEpoch.Ticks;

            data.deviceId = 18;
            data.entityId = 27;
            data.level = "edge";
            data.rawAccel = accel;

            //string j = @"{""deviceId"":529, ""obj"": {""x"": 534} }";

            //var json = JsonSerializer.Deserialize<Object>(j);

            //return JArray.FromObject(rdb.GetAll("TblRawAccel"));
            return JObject.FromObject(data);

        }

        // GET api/<devSotl>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.Error($"devSotl /0 esc: {ex.Message}");
                return "Exception";
            }
            return "devSotl not working at this time";
            
        }

        // POST api/<devSotl>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            var data = JsonSerializer.Deserialize<sotl6mdpack>(value);
            
            string r = (data is not null) ? $"data = {data.body}, from {data.deviceId}" : "No data";

            return r;
        }

        // PUT api/<devSotl>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<devSotl>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
