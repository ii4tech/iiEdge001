using Newtonsoft.Json.Linq;
using RavenTestApi.Entities;
using System.Collections;
using System.Collections.Generic;
using Dapper;

namespace RavenTestApi.Models
{
    public class TEntity
    {
        public IiTblLogs qiTblLogs = new IiTblLogs();
        public TblRawAccel tblRawAccel = new TblRawAccel();
        public TblSotlAccel tblSotlAccel =new TblSotlAccel();
        
    }
}
