using MongoDB.Driver;
using System;

namespace MongoLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //connection
            MongoUrlBuilder mongoUrlBuilder = new MongoUrlBuilder();
            mongoUrlBuilder.Scheme = MongoDB.Driver.Core.Configuration.ConnectionStringScheme.MongoDB;
            mongoUrlBuilder.Server = new MongoServerAddress("localhost", 27017);
            var clientSettings = mongoUrlBuilder.ToMongoUrl();
            MongoClient mongoClient = new MongoClient(clientSettings);

            var db = mongoClient.GetDatabase("Lab3");
            string content = "";
            var collection = db.GetCollection<Resturants>("resturants");
            collection.InsertMany()


        }
    }
}
