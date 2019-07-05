using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoCRM.Schema
{
    public class Contact : IModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoID { get; set; } 
        [BsonRepresentation(BsonType.String)]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string PrimaryPhone { get; set; }
        public Address Address { get; set; }

        public string CollectionName { get => "Contact";}
    }
}
