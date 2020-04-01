using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Domain.Entities;
using AutoMapper;
using Common.Events;
using Common.Exceptions;
using Common.Repositories;

namespace Api.Domain.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMongoRepository<Activity> mongoRepository;
        private readonly IMapper mapper;

        public ActivityService(IMongoRepository<Activity> mongoRepository, IMapper mapper)
        {
            this.mongoRepository = mongoRepository;
            this.mapper = mapper;
        }
        public async Task AddAsync(ActivityCreatedEvent activityEvent)
        {
            if (activityEvent == null) throw new CustomException("empty_activity", $"Empty Activity");

            var activity = mapper.Map<Activity>(activityEvent);
            await mongoRepository.InsertAsync(activity);
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> searchTerm)
        {
            var activities = await mongoRepository.FindAllAsync(searchTerm)
                                  ?? throw new CustomException("invalid_activities", $"Activities not found");
            return activities;
        }

        public async Task<Activity> GetAsync(Expression<Func<Activity, bool>> searchTerm)
        {
            var activities = await mongoRepository.FindAsync(searchTerm)
                                  ?? throw new CustomException("invalid_activity", $"Activity not found");
            return activities;
        }
    }
}
