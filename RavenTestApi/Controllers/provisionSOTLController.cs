using Microsoft.AspNetCore.Mvc;
using RavenTestApi.Entities;
using RavenTestApi.Entities.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RavenTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class provisionSOTLController : ControllerBase
    {
        // GET: api/<provisionSOTLController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<provisionSOTLController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<provisionSOTLController>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            //Instantiate deviceinfo with the key
            QryTblDeviceInfo deviceinfo = new QryTblDeviceInfo(value);

            //Provisioning already occured and the deviceinfo object is filled.
            //Calling the Insert method returns the certificate for the device
            string resp = deviceinfo.InsertRaven();

            //return the certificate for the device to use to interact with the server.
            return resp;

        }

        // PUT api/<provisionSOTLController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<provisionSOTLController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
