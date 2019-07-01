using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.Repository.Constants;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;

        public RoleRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleViewModel>> GetRoles(int userID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@USERID", userID);
                    return await conn.QueryAsync<RoleViewModel>(SPConstants.getRoles, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddRole(RoleViewModel role)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();                    
                    param.Add("@ROLENAME", role.ROLENAME);
                    param.Add("@COPYFROM", role.COPYFROM);
                    param.Add("@COMPANYID", role.COMPANYID);
                    param.Add("@DESCRIPTION", role.DESCRIPTION);
                    param.Add("@STATUS", role.STATUS);
                    param.Add("@CREATEDBY", role.CREATEDBY);
                    await conn.ExecuteAsync(SPConstants.CreateUser, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRole(RoleViewModel role)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@UPDATEDBY", role.UPDATEDBY);
                    param.Add("@ROLEID", role.ROLEID);                    
                    await conn.ExecuteAsync(SPConstants.DeleteRole, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRole(RoleViewModel role)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@ROLENAME", role.UPDATEDBY);
                    param.Add("@DESCRIPTION", role.DESCRIPTION);
                    param.Add("@STATUS", role.STATUS);
                    param.Add("@ROLEID", role.ROLEID);
                    param.Add("@UPDATEDBY", role.UPDATEDBY);
                    await conn.ExecuteAsync(SPConstants.UpdateRole, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Roles Status
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleStatus(RoleViewModel role)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@ROLEID", role.ROLEID);
                    param.Add("@UPDATEDBY", role.UPDATEDBY);
                    await conn.ExecuteAsync(SPConstants.UpdateRoleStatus, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Role Permission
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> GetRolePermission(RoleViewModel role)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@ROLEID", role.ROLEID);
                    await conn.ExecuteAsync(SPConstants.GetRolePermission, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get User Role Assignment
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> GetUserRoleAssignment(RoleViewModel role)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@USERID", role.USERID);
                    param.Add("@ROLEID", role.ROLEID);
                    await conn.ExecuteAsync(SPConstants.GetUserRoleAssignment, param, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}
