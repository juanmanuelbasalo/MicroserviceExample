using Activities.Domain.Entities;
using AutoMapper;
using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
            => CreateMap<Activity,CreateActivityCommand>();
    }
}
