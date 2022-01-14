using Newtonsoft.Json.Linq;
using Raven.Client.Documents;
using Raven.Embedded;
using RavenTestApi.DbClients.rdbstore;
using RavenTestApi.Entities;
using RavenTestApi.Models;
using RavenTestApi.Services;
using Serilog;

namespace RavenTestApi.DbClients
{
    public class RavenDb : IGenericRepository
    {

        private string rdbname = DatabaseConfig.dbname;
        private IDocumentStore store;
        public RavenDb()
        {

            EmbeddedServer.Instance.StartServer(new ServerOptions
            {
                CommandLineArgs = new List<string>
                    {
                    "--License.Path=secure/rvnlic.json"
                    }
            });

            EmbeddedServer.Instance.StartServer();
            //EmbeddedServer.Instance.OpenStudioInBrowser();

            store = DocumentStoreHolder.Store;
            try
            {
                using (var session = store.OpenSession())
                {
                    TblRawAccel accel = session
                        .Load<TblRawAccel>("Time/1");

                    if (accel.Time == 0)
                    {

                        session.Store(new TblRawAccel
                        {
                            Time = Util.getEpoch(DateTime.UtcNow),
                            X = 351,
                            Y = 425,
                            Z = 785
                        });

                        // send all pending operations to server, in this case only `Put` operation
                        session.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error getting or saving on startup: {ex.Message}");
            }
                       
        }

        public async Task<JObject> GetByIdAsync(Record record)
        {
            JObject json = new JObject();
            using (var store = EmbeddedServer.Instance.GetDocumentStore(rdbname))
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return json;
        }

        public JArray GetAll(string table)
        {
            JArray array = new JArray();
            using (var store = EmbeddedServer.Instance.GetDocumentStore(rdbname))
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return array;
        }

        public async Task<int> AddAsync(string query)
        {
            using (var store = EmbeddedServer.Instance.GetDocumentStore(rdbname))
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return 0;
        }

        public async Task<int> UpdateAsync(string query)
        {
            using (var store = EmbeddedServer.Instance.GetDocumentStore(rdbname))
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return 0;
        }

        public async Task<int> DeleteAsync(Record record)
        {
            using (var store = EmbeddedServer.Instance.GetDocumentStore(rdbname))
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return 0;
        }

        public async Task<int> BulkInsert(string query)
        {
            using (var store = EmbeddedServer.Instance.GetDocumentStore(rdbname))
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return 0;
        }

    }
}
