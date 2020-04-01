﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get;  set; }
        public Guid UserId { get;  set; }
        public string Category { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string CreatedBy { get;  set; }
        public DateTime CreatedAt { get;  set; }
    }
}
