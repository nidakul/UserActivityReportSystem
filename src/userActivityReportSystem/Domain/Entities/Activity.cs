using NArchitecture.Core.Persistence.Repositories;
using System;
namespace Domain.Entities
{
    public class Activity : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public Activity()
        {
        }
    }
}

