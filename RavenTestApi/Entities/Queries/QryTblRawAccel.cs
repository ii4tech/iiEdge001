using Newtonsoft.Json.Linq;
using Raven.Client.Documents;
using RavenTestApi.DbClients.rdbstore;
using RavenTestApi.DTO;
using RavenTestApi.Models;
using Serilog;
using System.Text.Json;

namespace RavenTestApi.Entities.Queries
{
    public static class QryTblRawAccel
    {
        
        public static int InsertRaven(string data)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            TblRawAccel? type = JsonSerializer.Deserialize<TblRawAccel>(data);

            using (store)
            {
                using (var session = store.OpenSession())
                {
                    session.Store(type);

                    // send all pending operations to server, in this case only `Put` operation
                    session.SaveChanges();
                }
            }

            return 0;
        }

        public static JArray GetAccelById(string deviceId)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            
            using (var session = store.OpenSession())
            {
                                
                try
                {
                    //Beautiful Linq query returning just the fields that I need.
                    var accels = from accel in session.Query<TblRawAccel>()
                                 where accel.deviceId == deviceId
                                 select new
                                 {
                                     accel.Time,
                                     accel.X,
                                     accel.Y,
                                     accel.Z
                                 };

                    ////Doesn't work but would be cool if it did.
                    //    IList<RawAccelDTO> results = session
                    //    .Advanced
                    //    .DocumentQuery<TblRawAccel>()
                    //    .WhereEquals(x => x.deviceId, deviceId)
                    //    .SelectFields<RawAccelDTO>()
                    //    .ToList();

                     return JArray.FromObject(accels);
                }
                catch (Exception ex)
                {
                    Log.Information($"//Exception in GetAccelById, ex = {ex.Message}");
                }
                return new JArray();
            }
           
        }

        public static int DeleteRavenById(string deviceId)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            using (store)
            {
                using (var session = store.OpenSession())
                {
                    session.Delete($"accels/{deviceId}");
                    session.SaveChanges();
                    return 0;
                }
            }
        }

    }
}
