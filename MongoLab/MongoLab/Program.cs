using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

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

            var restaurantCollection = db.GetCollection<Restaurant>("resturants");

            List<Restaurant> restaurants = CreateRestaurants();

            foreach (var restaurant in restaurants)
            {
                restaurantCollection.InsertOne(restaurant);
            }

            

        }

        private static List<Restaurant> CreateRestaurants()
        {
            return new List<Restaurant>()
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
                }
            };
        }
    }
}
