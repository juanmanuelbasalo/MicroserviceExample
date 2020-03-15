using Activities.Domain.Entities;
using Activities.Domain.Repositories;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IRepository<Activity> activityRepository;
        private readonly IRepository<Category> categoryRepository;
        public ActivityService(IRepository<Activity> activityRepository, IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.activityRepository = activityRepository;
        }

        public async Task AddAsync(Activity activity)
        {
            var activityCategory = await categoryRepository.FindAsync(i => i.Name.Equals(activity.Category.Name)) 
                ?? throw new CustomException("category_not_found",$"Category: {activity.Category.Name} was not found");

            await activityRepository.InsertAsync(activity);
        }
        public async Task<Activity> GetAsync(string name)
            => await activityRepository.FindAsync(i => i.Name.Equals(name.ToLowerInvariant()));
    }
}
