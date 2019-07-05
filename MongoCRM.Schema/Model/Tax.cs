using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoCRM.Schema
{
    public class Tax : IModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoID { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid ID { get; set; }        
        public string Name { get; set; }        
        public long Percentage { get; set; }        
        public string Description { get; set; }      
        public string Type { get; set; }       
        public string Status { get; set; }

        public string CollectionName => "Tax";
    }
}
