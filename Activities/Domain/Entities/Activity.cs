using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Category { get; protected set; }
        [Required(ErrorMessage = "Activity name can not be empty.")]
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Activity(Guid id, Guid userId, string category, string name, string description, string createdBy, DateTime createdAt)
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
