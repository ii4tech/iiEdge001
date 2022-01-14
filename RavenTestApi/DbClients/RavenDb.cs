using Newtonsoft.Json.Linq;
using Raven.Client.Documents;
using Raven.Embedded;
using RavenTestApi.DbClients.rdbstore;
using RavenTestApi.Entities;
using RavenTestApi.Models;
using RavenTestApi.Services;
using Serilog;
using System.Text.Json;

namespace RavenTestApi.DbClients
{
    public class RavenDb
    {

        private IDocumentStore store;
        public RavenDb()
        {

            store = DocumentStoreHolder.Store;
        
        }

        public async Task<JObject> GetByIdAsync(Item item)
        {
            JObject json = new JObject();
            using (store)
            {
                using (var session = store.OpenSession())
                {
                    // Your code here
                }
            }

            return json;
        }

        public List<TblRawAccel> GetAllAccels()
        {
            JArray array = new JArray();
            
            using (var session = store.OpenSession())
            {
                   
                try
                {
                    List<TblRawAccel>? accel = session
                                    .Advanced
                                    .DocumentQuery<TblRawAccel>()
                                    .ToList();

                    if (accel.Count == 0)
                    {

                        session.Store(new TblRawAccel
                        {
                            deviceId = "dhetest1",
                            Time = Util.getEpoch(DateTime.UtcNow),
                            X = 351,
                            Y = 425,
                            Z = 785
                        });

                        // send all pending operations to server, in this case only `Put` operation
                        session.SaveChanges();

                        accel = session
                                    .Advanced
                                    .DocumentQuery<TblRawAccel>()
                                    .ToList();
                    }

                    return accel;
                }
                catch (Exception ex)
                {
                    Log.Error($"Error getting or saving on startup: {ex.Message}");

                    return new List<TblRawAccel>();
                }

            }

        }

        public JArray GetAllLogs()
        {
            JArray array = new JArray();

            using (var session = store.OpenSession())
            {

                try
                {
                    List<IiTblLogs>? accel = session
                                    .Advanced
                                    .DocumentQuery<IiTblLogs>()
                                    .ToList();

                    if (accel.Count == 0)
                    {

                        session.Store(new IiTblLogs
                        {
                            
                            Time = Util.FormatDateTime(DateTime.UtcNow),
                            Message = "A fake exception",
                            Program = "RavenTestApi",
                            tTag = "RavenDb",
                            ExceptionText = "This is a test",
                            ExceptionCode = 20,
                            Level = "Information",
                            User = "Developer"
                        });

                        // send all pending operations to server, in this case only `Put` operation
                        session.SaveChanges();

                        accel = session
                                    .Advanced
                                    .DocumentQuery<IiTblLogs>()
                                    .ToList();
                    }
                    array = JArray.FromObject(accel);
                }
                catch (Exception ex)
                {
                    Log.Error($"Error getting or saving on startup: {ex.Message}");

                }

            }

            return array;
        }

        public async Task<int> AddAsync(Record query)
        {
           
            

            return 0;
        }

        public async Task<int> UpdateAsync(string query)
        {
            
            using (var session = store.OpenSession())
            {
                // Your code here
            }
            

            return 0;
        }

        public async Task<int> DeleteAsync(Item item)
        {
            
            using (var session = store.OpenSession())
            {
                // Your code here
            }            

            return 0;
        }

        public async Task<int> BulkInsert(string query)
        {
            
            using (var session = store.OpenSession())
            {
                // Your code here
            }
           
            return 0;
        }

    }
}
