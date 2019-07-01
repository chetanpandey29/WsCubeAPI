using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.Factory;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;
        ILoggerFactory _loggerFactory;
        ILogger _logger;

        public RoleService(IRoleRepository roleRepository, ILoggerFactory loggerFactory)
        {
            this._roleRepository = roleRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleViewModel>> GetRoles(int userID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            if (string.IsNullOrEmpty(userID.ToString()))
                return null;

            var user = await _roleRepository.GetRoles(userID);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddRole(RoleViewModel role)
        {
            if (role == null)
                return false;

            var flag = await _roleRepository.AddRole(role);
            return true;
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRole(RoleViewModel role)
        {
            if (role == null)
                return false;

            var flag = await _roleRepository.DeleteRole(role);
            return true;
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRole(RoleViewModel role)
        {
            if (role == null)
                return false;

            var flag = await _roleRepository.UpdateRole(role);
            return true;
        }

        /// <summary>
        /// Update Role Status
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleStatus(RoleViewModel role)
        {
            if (role == null)
                return false;

            var flag = await _roleRepository.UpdateRoleStatus(role);
            return true;
        }

        /// <summary>
        /// Get Role Permission
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> GetRolePermission(RoleViewModel role)
        {
            if (role == null)
                return false;

            var flag = await _roleRepository.GetRolePermission(role);
            return true;
        }

        /// <summary>
        /// Get UserRole Assignment 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> GetUserRoleAssignment(RoleViewModel role)
        {
            if (role == null)
                return false;

            var flag = await _roleRepository.GetUserRoleAssignment(role);
            return true;
        }
    }
}
