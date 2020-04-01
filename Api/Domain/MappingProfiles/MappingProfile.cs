using Api.Domain.Entities;
using AutoMapper;
using Common.Commands;
using Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActivityCreatedEvent, Activity>().ReverseMap();
        }
    }
}
