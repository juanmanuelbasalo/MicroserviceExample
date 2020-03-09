using Activities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.Services
{
    public interface IActivityService
    {
        Task<Activity> GetAsync(string name);
        Task<Activity> AddAsync(Activity category);
    }
}
