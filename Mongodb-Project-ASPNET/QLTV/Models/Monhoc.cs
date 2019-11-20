using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTV.Models
{
    public class Monhoc
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public int MonhocId { get; set; }
        [BsonElement]
        public string MaMH { get; set; }
        [BsonElement]
        public string TenMH { get; set; }
        [BsonElement]
        public string KhoaQL { get; set; }
    }
}
