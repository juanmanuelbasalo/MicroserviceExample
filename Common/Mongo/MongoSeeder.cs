using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase mongoDb;

        public MongoSeeder(IMongoDatabase mongoDb)
        {
            this.mongoDb = mongoDb;
        }
        public async Task SeedAsync()
        {
            var collectionCursor = await mongoDb.ListCollectionsAsync();
            var collection = await collectionCursor.ToListAsync();
            if (collection.Any()) return;

            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
