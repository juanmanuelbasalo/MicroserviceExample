using Api.Domain.Entities;
using Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface IActivityService
    {
        Task<Activity> GetAsync(Expression<Func<Activity, bool>> searchTerm);
        Task AddAsync(ActivityCreatedEvent activity);
        Task<IEnumerable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> searchTerm);
    }
}
