using Raven.Client.Documents;

namespace RavenTestApi.DbClients.rdbstore
{
    public class DocumentStoreHolder
    {
        // Use Lazy<IDocumentStore> to initialize the document store lazily. 
        // This ensures that it is created only once - when first accessing the public `Store` property.
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                // Define the cluster node URLs (required)
                Urls = new[] { "http://127.0.0.1", 
                           /*some additional nodes of this cluster*/ },

                // Set conventions as necessary (optional)
            ////Conventions =
            ////{
            ////    MaxNumberOfRequestsPerSession = 10,
            ////    UseOptimisticConcurrency = true
            ////},

                // Define a default database (optional)
                Database = DatabaseConfig.rdbname,

                // Define a client certificate (optional)
                //Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),

                // Initialize the Document Store
            }.Initialize();

            return store;
        }
    }
}
