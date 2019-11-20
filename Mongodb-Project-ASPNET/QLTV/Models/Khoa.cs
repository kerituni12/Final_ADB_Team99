using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTV.Models
{
    public class Khoa
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public int KhoaId { get; set; }
        [BsonElement]
        public string KhoaName { get; set; }
        [BsonElement]
        public string Address { get; set; }
    }
}
