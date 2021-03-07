using System;

namespace RollingRetention.Shared.Models
{
    public class UserRetentionDto
    {
        public DateTime Day { get; set; }
        public int LiveUsers { get; set; }
    }
}