using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Gringotts.Core.Model
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
