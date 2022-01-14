using Newtonsoft.Json.Linq;
using Raven.Client.Documents;
using RavenTestApi.DbClients.rdbstore;
using RavenTestApi.Models;
using System.Text.Json;

namespace RavenTestApi.Entities.Queries
{
    public static class QryIiTblLogs
    {
       
        public static int InsertRaven(string data)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            IiTblLogs? row = JsonSerializer.Deserialize<IiTblLogs>(data);

            using (store)
            {
                using (var session = store.OpenSession())
                {
                    session.Store(row);

                    // send all pending operations to server, in this case only `Put` operation
                    session.SaveChanges();
                }
            }

            return 0;

        }

        public static JArray GetRavenById(string deviceId, string type)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            using (store)
            {
                using (var session = store.OpenSession())
                {
                    JArray accel = JArray.FromObject(session.Load<IiTblLogs>(new[]
                    {
                        $"{type}/{deviceId}"
                    }));

                    return accel;
                }
            }
        }
        public static int DeleteRavenById(string deviceId, string type)
        {
            IDocumentStore store = DocumentStoreHolder.Store;
            using (store)
            {
                using (var session = store.OpenSession())
                {
                    session.Delete($"{type}/{deviceId}");
                    session.SaveChanges();
                    return 0;
                }
            }
        }

    }
}
