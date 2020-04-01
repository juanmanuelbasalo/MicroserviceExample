using Activities.Domain.Entities;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoRepository<Category> repository;
        public CategoryService(IMongoRepository<Category> repository)
        {
            this.repository = repository;
        }
        public async Task AddAsync(Category category)
        {
            await repository.InsertAsync(category);
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await repository.GetAllAsync();

        public async Task<Category> GetAsync(string name)
            => await repository.FindAsync(i => i.Name.Equals(name.ToLowerInvariant()));

    }
}
