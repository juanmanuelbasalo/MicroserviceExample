using Activities.Domain.Entities;
using Activities.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;
        public CategoryService(IRepository<Category> repository)
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
