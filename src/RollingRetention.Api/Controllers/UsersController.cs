using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RollingRetention.Core.Entities;
using RollingRetention.Core.Interfaces;
using RollingRetention.Shared.Models;

namespace RollingRetention.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = "client")]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRollingRetentionService _rollingRetention;

        public UsersController(UserManager<ApplicationUser> userManager,
            IRollingRetentionService rollingRetention)
        {
            _userManager = userManager;
            _rollingRetention = rollingRetention;
        }

        // GET: api/users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/users
        /// 
        /// </remarks>
        /// <returns>List of User DTO schemes</returns>
        /// <response code="200">Successful API response</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var usersDb = await _userManager.Users.ToListAsync();

            return usersDb.Select(userEntity => new UserDto()
            {
                Id = userEntity.Id, 
                UserName = userEntity.UserName, 
                RegistrationDate = userEntity.RegistrationDate, 
                LastActivityDate = userEntity.LastActivityDate
            }).ToList();
        }

        /// <summary>
        /// Get calculated user live retentions
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/users/retentions/7
        /// 
        /// </remarks>
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        [HttpGet("retentions/{days}")]
        [ProducesResponseType(typeof(IEnumerable<UserRetentionDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserRetentionDto>> GetUserRetentions(int days)
        {
            var usersDb = await _userManager.Users.ToListAsync();
            return _rollingRetention.CalculateLiveRetentions(usersDb, days);
        }

        // GET api/users/{id}
        /// <summary>
        /// Get user with specified ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/users/ab123a45-6d78-9030-af00-0e0c0c0fdaa0
        /// 
        /// </remarks>
        /// <param name="userId">User ID</param>
        /// <returns>User DTO scheme</returns>
        /// <response code="200">Successful API response</response>
        /// <response code="400">Bad request API response which indicates user could not found with specified UserID</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);

            if (userEntity == null)
            {
                return BadRequest($"Could not found user with ID {userId}");
            }

            var userDto = new UserDto()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                RegistrationDate = userEntity.RegistrationDate,
                LastActivityDate = userEntity.LastActivityDate
            };
            return Ok(userDto);
        }

        // PUT api/users/{id}
        /// <summary>
        /// Update user registration date and last activity date
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/users/ab123a45-6d78-9030-af00-0e0c0c0fdaa0
        ///     {
        ///         RegistrationDate = "14/03/2019",
        ///         LastActivityDate = "18/03/2020",
        ///     }
        /// 
        /// </remarks>
        /// <param name="userId">User ID</param>
        /// <param name="userDto">User data transfer object</param>
        /// <response code="200">Successful API response which indicates user details updated successfully</response>
        /// <response code="400">Bad request API response which indicates user could not found with specified ID</response>
        /// <returns></returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDto userDto)
        {
            var userEntity = await _userManager.FindByIdAsync(userId);

            if (userEntity == null)
            {
                return BadRequest($"Could not found user with ID {userId}");
            }

            userEntity.RegistrationDate = userDto.RegistrationDate;
            userEntity.LastActivityDate = userDto.LastActivityDate;
            await _userManager.UpdateAsync(userEntity);

            return Ok("User details updated successfully");
        }
    }
}
