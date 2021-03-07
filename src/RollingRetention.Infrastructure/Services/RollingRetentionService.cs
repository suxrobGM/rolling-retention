using System;
using System.Collections.Generic;
using System.Linq;
using RollingRetention.Core.Entities;
using RollingRetention.Core.Interfaces;
using RollingRetention.Shared.Models;

namespace RollingRetention.Infrastructure.Services
{
    public class RollingRetentionService : IRollingRetentionService
    {
        public float CalculateRollingRetention(IList<ApplicationUser> users, int day)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserRetentionDto> CalculateLiveRetentions(IList<ApplicationUser> users, int days)
        {
            var startingDay = DateTime.Now.AddDays(-days);
            var userRetentions = new List<UserRetentionDto>();

            for (var i = 0; i < days; i++)
            {
                var day = startingDay.AddDays(i);
                var liveUsers = users.Count(user => user.RegistrationDate <= day && user.LastActivityDate >= day);

                userRetentions.Add(new UserRetentionDto()
                {
                    LiveUsers = liveUsers,
                    Day = day
                });
            }

            return userRetentions;
        }
    }
}