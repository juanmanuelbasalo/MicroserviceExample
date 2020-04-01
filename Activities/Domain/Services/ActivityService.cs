using Activities.Domain.Entities;
using Common.Exceptions;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMongoRepository<Activity> activityRepository;
        private readonly IMongoRepository<Category> categoryRepository;
        public ActivityService(IMongoRepository<Activity> activityRepository, IMongoRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.activityRepository = activityRepository;
        }

        public async Task AddAsync(Activity activity)
        {
            var activityCategory = await categoryRepository.FindAsync(i => i.Name.Equals(activity.Category)) 
                ?? throw new CustomException("category_not_found",$"Category: {activity.Category} was not found");

            await activityRepository.InsertAsync(activity);
        }
        public async Task<Activity> GetAsync(string name)
            => await activityRepository.FindAsync(i => i.Name.Equals(name.ToLowerInvariant()));
    }
}
