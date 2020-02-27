using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public class ActivityCreatedEvent : IAuthenticatedEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public string CreatedBy { get; }
        public DateTime CreatedAt { get; }

        public ActivityCreatedEvent(Guid id, Guid userId, string category, string name
            ,string description, string createdBy, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
