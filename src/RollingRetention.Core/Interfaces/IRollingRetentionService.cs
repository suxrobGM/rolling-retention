using System.Collections.Generic;
using RollingRetention.Core.Entities;
using RollingRetention.Shared.Models;

namespace RollingRetention.Core.Interfaces
{
    public interface IRollingRetentionService
    {
        float CalculateRollingRetention(IList<ApplicationUser> users, int day);

        IEnumerable<UserRetentionDto> CalculateLiveRetentions(IList<ApplicationUser> users, int days);
    }
}
