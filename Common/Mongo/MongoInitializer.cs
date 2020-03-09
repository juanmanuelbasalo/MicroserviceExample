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

        public MongoInitializer(IOptions<MongoOptions> options, IMongoDatabase database)
        {
            this.database = database;
            seed = options.Value.Seed;
        }
        public async Task InitializeAsync()
        {
            if (initialized) return;
            RegisterConventions();
            initialized = true;
            if (!seed) return;
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
