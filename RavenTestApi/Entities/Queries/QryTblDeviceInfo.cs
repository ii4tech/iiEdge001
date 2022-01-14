using Raven.Client.Documents;
using RavenTestApi.DbClients.rdbstore;
using RavenTestApi.Services;
using System.Security.Cryptography;
using System.Text;

namespace RavenTestApi.Entities.Queries
{
    public class QryTblDeviceInfo
    {
        private TblDeviceInfo _entity = new TblDeviceInfo();
        private IDocumentStore store = DocumentStoreHolder.Store;
        private string Key = "";
        private string Id = "";

        public QryTblDeviceInfo(string key)
        {
            Key = key;
            ProvisionDevice();
        }

        public void ProvisionDevice()
        {
            
            string[] info = Key.Split('.');
            // Convert the input string to a byte array and compute the hash.
            SHA256 sha256Hash = SHA256.Create();
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(info[1]));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            
            Id = sBuilder.ToString();
            _entity.id = Id;
            _entity.name = Id.Substring(Id.Length - 5, 4);
            _entity.devicetype = info[0];
            _entity.devicekey = info[1];
            _entity.edgeid = "GetTheEdgeId";
            _entity.dateprovisioned = Util.FormatDateTime(DateTime.UtcNow);
            _entity.lastping = Util.FormatDateTime(DateTime.UtcNow);

        }

        public string InsertRaven()
        {
           
                using (var session = store.OpenSession())
                {
                    session.Store(_entity);

                    // send all pending operations to server, in this case only `Put` operation
                    session.SaveChanges();
                }
           
            return Id;
        }
    }
}
