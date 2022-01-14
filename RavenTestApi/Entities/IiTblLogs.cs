using Dapper.Contrib.Extensions;
using Newtonsoft.Json.Linq;
using RavenTestApi.Models;

namespace RavenTestApi.Entities
{
    [Table("QiLogs")]
    public class IiTblLogs
    {
        
        public string Time { get; set; }
        public String Message { get; set; }
        public string Program { get; set; }
        public string tTag { get; set; }
        public string ExceptionText { get; set; }
        public int ExceptionCode { get; set; }
        public string Level { get; set; }
        public string User { get; set; }
        
    }
}
