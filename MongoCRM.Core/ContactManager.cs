using MongoCRM.Schema;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCRM.Core
{
    public class ContactManager
    {

        public async Task CreateAsync(List<Contact> contacts)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");
            await contact.InsertManyAsync(contacts);
        }

        public async Task<Contact> GetByIDAsync(Guid id)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");
            var res =  await contact.FindAsync(p => p.ID == id);

            return res.FirstOrDefault();
        }


        public async Task<List<Contact>> GetAsync()
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");
            var cursor = await contact.FindAsync(p => true);
            return cursor.ToList();
        }

        public async Task<IAsyncCursor<Contact>> GetByMongoIDAsync(string mongoID)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");
            return await contact.FindAsync(p => p.MongoID == mongoID);
        }

        public async Task UpdateAsync(List<Contact> contactsToDelete)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");


            contactsToDelete.ForEach(async item =>
            {
                var filter = Builders<Contact>.Filter.Eq(x => x.MongoID, item.MongoID);
                await contact.ReplaceOneAsync(filter, item);
            }); 
            return;
        }

        public async Task<DeleteResult> DeleteAsync(List<Contact> contactsToDelete)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");
            var filter = Builders<Contact>.Filter.In("_id", contactsToDelete.Select(p => p.MongoID));
            return await contact.DeleteManyAsync(filter); 
        }


        public async Task<DeleteResult> DeleteByIDsAsync(List<Guid> contactsToDelete)
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("CRM");
            IMongoCollection<Contact> contact = db.GetCollection<Contact>("Contact");
            var filter = Builders<Contact>.Filter.In("ID", contactsToDelete);
            return await contact.DeleteManyAsync(filter);
        }

    }
}