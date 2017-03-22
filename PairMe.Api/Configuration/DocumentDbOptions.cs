namespace PairMe.Api.Configuration
{
    public class DocumentDbOptions
    {
        public string AccountEndpoint { get; set; }
        public string AccountKey { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}