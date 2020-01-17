using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoLab
{
    class Restaurant
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("stars")]
        public int Stars { get; set; }
        [BsonElement("categories")]
        public List<string> Categories { get; set; }

    }
}
