using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace PairMe.Api.Configuration
{
    public class DocumentDbProvisioning
    {
        private readonly DocumentClient client;

        public DocumentDbProvisioning(DocumentClient client)
        {
            this.client = client;
        }

        public async Task CreateDatabaseIfNotExists(string name)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(name));
            }
            catch (DocumentClientException exception)
            {
                if (exception.StatusCode != HttpStatusCode.NotFound) throw;

                await client.CreateDatabaseAsync(new Database { Id = name });
            }
        }

        public async Task CreateCollectionIfNotExists(string database, string collection)
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(database, collection));
            }
            catch (DocumentClientException exception)
            {
                if (exception.StatusCode != HttpStatusCode.NotFound) throw;

                // ToDo: Look at indexing policies
                var collectionInfo = new DocumentCollection
                {
                    Id = collection,
                    IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
                };

                // ToDo: Look at throughput options
                await client.CreateDocumentCollectionAsync(
                    UriFactory.CreateDatabaseUri(database),
                    collectionInfo,
                    new RequestOptions { OfferThroughput = 400 });
            }
        }
    }
}