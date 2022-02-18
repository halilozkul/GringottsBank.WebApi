using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Gringotts.Core.Model
{
    public class AccountDetail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string AccountNumber { get; set; }
        public string CustomerNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
