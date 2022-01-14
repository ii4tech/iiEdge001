using Dapper.Contrib.Extensions;
using Newtonsoft.Json.Linq;
using RavenTestApi.Models;

namespace RavenTestApi.Entities
{
    [Table("RawAccel")]
    public class TblRawAccel
    {  
        public string deviceId { get; set; }
        public Int64 Time { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

    }
}
