using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WS_Cube.Factory;
using System.Threading.Tasks;
using WS_Cube.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WS_CubeAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public class GroupController : ControllerBase
    {
        private IGroupService _groupService;
        ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="groupService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public GroupController(IGroupService groupService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _groupService = groupService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        #region Group

        /// <summary>
        /// Get Group List
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="groupTypeID"></param>
        /// <returns></returns>
        [HttpPost("{languageCode},{groupTypeID}")]
        public async Task<IActionResult> GetGroupList(int languageCode, int groupTypeID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _groupService.GetGroupList(languageCode, groupTypeID);
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get Group Type
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        [HttpPost("{languageID}")]
        public async Task<IActionResult> GetGroupTypeWithMandatory(int languageID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _groupService.GetGroupTypeWithMandatory(languageID);
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get User Group
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost("GroupUser/{languageID},{userID}")]
        public async Task<IActionResult> GetGroupsUserGroups(int languageID, int userID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _groupService.GetGroupsUserGroups(languageID, userID);
                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Create Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody]GroupViewModel group)
        {
            try
            {
                _groupService.Create(group);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update Group Assignment
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost("updategroupassignment")]
        public IActionResult UpdateGroupAssignment([FromBody]GroupViewModel group)
        {
            try
            {
                _groupService.UpdateGroupAssignment(group);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Change Group Status
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost("changegroupstatus")]
        public IActionResult ChangeGroupStatus([FromBody]GroupViewModel group)
        {
            try
            {
                _groupService.ChangeGroupStatus(group);
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