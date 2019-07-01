using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WS_Cube.Factory;
using System.Threading.Tasks;
using WS_Cube.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WS_CubeAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public UsersController(IUserService userService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        #region User
        /// <summary>
        /// Authentication Token
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserDto userDto)
        {
            try
            {
                var user = await _userService.GetUserByUserPassword(userDto.Username, userDto.Password);
                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                var tokenHandler = new JwtSecurityTokenHandler();
                var appSettingsSection = Configuration.GetSection("AppSettings");
                var appSettings = appSettingsSection.Get<AppSettings>();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new
                {
                    Id = user.UserId,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenString
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
      
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserInfo(int userID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _userService.GetUserInfo(userID);
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _userService.GetAllUsers();
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("UserProfile/{userID},{loggedUserID}")]
        public async Task<IActionResult> GetUserProfile(int userID, int loggedUserID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _userService.GetUsers(userID, loggedUserID);
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserListViewModel user)
        {
            try
            {
                _userService.Create(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpPost("{userID},{updatedBy}")]
        public async Task<IActionResult> DeleteUserInfo(int userID, string updatedBy)
        {
            try
            {
                var user = await _userService.DeleteUserInfo(userID, updatedBy);
                return Ok("Successfull");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public IActionResult Update([FromBody]UserListViewModel user)
        {
            try
            {
                _userService.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Add Target Multi User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("addtargetmultiuser")]
        public IActionResult AddTargetMultiUser([FromBody]UserListViewModel user)
        {
            try
            {
                _userService.AddTargetMultiUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion
    }
}