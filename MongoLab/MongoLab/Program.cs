using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MongoLab
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient mongoClient = EstablishConnection();
            var db = mongoClient.GetDatabase("Lab3");
            var collection = db.GetCollection<Restaurant>("restaurants");
            SeedDatabase(collection);


            PrintFullCollection(collection);

            PrintAllCafes(collection);

            IncrementStars(collection);
            PrintFullCollection(collection);

            string oldName = "456 Cookies Shop";
            string newName = "123 Cookies Heaven";
            UpdateName(collection, oldName, newName);
            PrintFullCollection(collection);

            PrintRestaurantsWithFourStars(collection);
        }
        private static void IncrementStars(IMongoCollection<Restaurant> collection)
        {
            var restaurantToUpdate = "XYZ Coffee Bar";
            var filter = Builders<Restaurant>.Filter.Eq("name", restaurantToUpdate);
            var update = Builders<Restaurant>.Update.Inc("stars", 1);
            collection.UpdateOne(filter, update);
        }
        private static void PrintRestaurantsWithFourStars(IMongoCollection<Restaurant> collection)
        {
            var filter = Builders<Restaurant>.Filter.Gte("stars", 4) ;
            var ratedRestaurants = collection.Find(filter).Project("{_id:0,name:1,stars:1}"); ;

            Console.WriteLine("Printing restaurants with 4stars..\n\n");
            foreach (var item in ratedRestaurants.ToEnumerable())
            {
                Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
        }
        private static void PrintAllCafes(IMongoCollection<Restaurant> collection)
        {
            Console.WriteLine("Printing all cafes..\n\n");
            var filter = Builders<Restaurant>.Filter.Eq("categories", "Cafe");
            var cafeCollection = collection.Find(filter).Project("{_id:0,name:1}");

            foreach (var item in cafeCollection.ToEnumerable())
            {
                Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
        }
        private static void PrintFullCollection(IMongoCollection<Restaurant> collection)
        {
            Console.WriteLine("Printing all restaurants..\n\n");
            var fullCollection = collection.Find(Builders<Restaurant>.Filter.Empty);

            foreach (var item in fullCollection.ToEnumerable())
            {
                Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
        }
        private static MongoClient EstablishConnection()
        {
            MongoUrlBuilder mongoUrlBuilder = new MongoUrlBuilder();
            mongoUrlBuilder.Scheme = MongoDB.Driver.Core.Configuration.ConnectionStringScheme.MongoDB;
            mongoUrlBuilder.Server = new MongoServerAddress("localhost", 27017);
            var clientSettings = mongoUrlBuilder.ToMongoUrl();
            return new MongoClient(clientSettings);
        }
        private static void UpdateName(IMongoCollection<Restaurant> collection, string nameToUpdate, string newName)
        {
            var builder = Builders<Restaurant>.Filter;
            var filter = builder.Eq("name", nameToUpdate);
            collection.UpdateOne(filter, newName);
        }
        private static void SeedDatabase(IMongoCollection<Restaurant> collection)
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Sun Bakery Trattoria",
                    Stars = 4,
                    Categories = new List<string>()
                    {
                        "Pizza", "Pasta", "Italian", "Coffee", "Sandwiches"
                    }
                },
                new Restaurant()
                {
                    Name = "Blue Bagels Grill",
                    Stars = 3,
                    Categories = new List<string>()
                    {
                        "Bagels", "Cookies", "Sandwiches"
                    }
                },
                new Restaurant()
                {
                    Name = "XYZ Coffee Bar",
                    Stars = 5,
                    Categories = new List<string>()
                    {
                        "Coffee", "Cafe", "Bakery", "Chocolates"
                    }
                },
                new Restaurant()
                {
                    Name = "456 Cookies Shop",
                    Stars = 4,
                    Categories = new List<string>()
                    {
                        "Bakery", "Cookies", "Cake", "Coffee"
                    }
                },
                new Restaurant()
                {
                    Name = "Hot Bakery Cafe",
                    Stars = 4,
                    Categories = new List<string>()
                    {
                        "Bakery", "Cafe", "Coffee", "Dessert"
                    }
                }
            };
            foreach (var restaurant in restaurants)
            {
                collection.InsertOne(restaurant);
            }
        }
    }
}
