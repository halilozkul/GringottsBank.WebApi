using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Core.Model
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
