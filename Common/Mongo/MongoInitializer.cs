using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool initialized;
        private readonly bool seed;
        private readonly IMongoDatabase database;
        private readonly IDatabaseSeeder databaseSeeder;

        public MongoInitializer(IOptions<MongoOptions> options, IMongoDatabase database, IDatabaseSeeder databaseSeeder)
        {
            this.database = database;
            seed = options.Value.Seed;
            this.databaseSeeder = databaseSeeder;
        }
        public async Task InitializeAsync()
        {
            if (initialized) return;
            RegisterConventions();
            initialized = true;
            if (!seed) return;
            await databaseSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("MicroServicesExampleConventions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
