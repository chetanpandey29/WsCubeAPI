using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.Factory;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository _groupRepository;

        ILoggerFactory _loggerFactory;
        ILogger _logger;

        /// <summary>
        /// Get Group List
        /// </summary>
        /// <param name="groupRepository"></param>
        /// <param name="loggerFactory"></param>
        public GroupService(IGroupRepository groupRepository, ILoggerFactory loggerFactory)
        {
            this._groupRepository = groupRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }

        /// <summary>
        /// Get Group List
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="groupTypeID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupViewModel>> GetGroupList(int languageCode, int groupTypeID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            var user = await _groupRepository.GetGroupList(languageCode, groupTypeID);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get Group Type
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupViewModel>> GetGroupTypeWithMandatory(int languageID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            var user = await _groupRepository.GetGroupTypeWithMandatory(languageID);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get User Groups
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupViewModel>> GetGroupsUserGroups(int languageID, int userID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            var user = await _groupRepository.GetGroupsUserGroups(languageID, userID);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }

        /// <summary>
        /// Create Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<bool> Create(GroupViewModel group)
        {
            if (group == null)
                return false;

            var flag = await _groupRepository.Create(group);
            return true;
        }

        /// <summary>
        /// Update Group Assignment
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<bool> UpdateGroupAssignment(GroupViewModel group)
        {
            if (group == null)
                return false;

            var flag = await _groupRepository.UpdateGroupAssignment(group);
            return true;
        }

        /// <summary>
        /// Change Group Status
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<bool> ChangeGroupStatus(GroupViewModel group)
        {
            if (group == null)
                return false;

            var flag = await _groupRepository.ChangeGroupStatus(group);
            return true;
        }
    }
}
