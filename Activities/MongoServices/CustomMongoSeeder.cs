using Activities.Domain.Entities;
using Activities.Domain.Services;
using Common.Mongo;
using Common.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.MongoServices
{
    public class CustomMongoSeeder : MongoSeeder 
    {
        private readonly IMongoRepository<Category> categoryRepository;
        public CustomMongoSeeder(IMongoDatabase mongoDb, IMongoRepository<Category> categoryRepository) : base(mongoDb)
            => this.categoryRepository = categoryRepository;

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };

            await Task.WhenAll(categories.Select(i => categoryRepository.InsertAsync(new Category(i))));
        }
    }
}
