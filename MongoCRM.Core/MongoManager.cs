using MongoCRM.Schema;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCRM.Core
{
    public class MongoManager<T> where T : IModelBase
    {
        MongoClient dbClient;
        IMongoDatabase db;

        public MongoManager(ICRMDatabaseSettings databaseConfig)
        {
            dbClient = new MongoClient(databaseConfig.ConnectionString);

            db = dbClient.GetDatabase(databaseConfig.DatabaseName);
        }

        public async Task CreateAsync(List<T> Ts)
        {
            IMongoCollection<T> T = db.GetCollection<T>(Ts.FirstOrDefault().CollectionName);
            await T.InsertManyAsync(Ts);
        }

        public async Task<T> GetByIDAsync(Guid id, string collectionName)
        {
            IMongoCollection<T> T = db.GetCollection<T>(collectionName);
            var res =  await T.FindAsync(p => p.ID == id);

            return res.FirstOrDefault();
        }


        public async Task<List<T>> GetAsync(string collectionName)
        {
            IMongoCollection<T> T = db.GetCollection<T>(collectionName);
            var cursor = await T.FindAsync(p => true);
            return cursor.ToList();
        }

        public async Task<IAsyncCursor<T>> GetByMongoIDAsync(string mongoID, string collectionName)
        {
            IMongoCollection<T> T = db.GetCollection<T>(collectionName);
            return await T.FindAsync(p => p.MongoID == mongoID);
        }

        public async Task UpdateAsync(List<T> Ts)
        {
            IMongoCollection<T> T = db.GetCollection<T>(Ts.FirstOrDefault().CollectionName);

            Ts.ForEach(async item =>
            {
                var filter = Builders<T>.Filter.Eq(x => x.MongoID, item.MongoID);
                await T.ReplaceOneAsync(filter, item);
            }); 
            return;
        }

        public async Task<DeleteResult> DeleteAsync(List<T> Ts)
        {
            IMongoCollection<T> T = db.GetCollection<T>(Ts.FirstOrDefault().CollectionName);

            var filter = Builders<T>.Filter.In("_id", Ts.Select(p => p.MongoID));
            return await T.DeleteManyAsync(filter); 
        }


        public async Task<DeleteResult> DeleteByIDsAsync(List<Guid> TsToDelete, string collectionName)
        {
            IMongoCollection<T> T = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.In("ID", TsToDelete);
            return await T.DeleteManyAsync(filter);
        }

    }
}