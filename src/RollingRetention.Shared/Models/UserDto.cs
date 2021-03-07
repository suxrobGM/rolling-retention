using System;

namespace RollingRetention.Shared.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}