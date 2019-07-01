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
    public class GroupRepository : IGroupRepository
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;

        public GroupRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
        }

        /// <summary>
        /// Get Group List
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="groupTypeID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupViewModel>> GetGroupList(int languageCode, int groupTypeID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@LANGUAGECODE", languageCode);
                    param.Add("@GROUPTYPEID", groupTypeID);
                    return await conn.QueryAsync<GroupViewModel>(SPConstants.getGrouplist, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get Group Type
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupViewModel>> GetGroupTypeWithMandatory(int languageID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@LANGUAGEID", languageID);
                    return await conn.QueryAsync<GroupViewModel>(SPConstants.getGroupTypewithMandatory, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get User Groups
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GroupViewModel>> GetGroupsUserGroups(int languageID, int userID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@LANGUAGEID", languageID);
                    param.Add("@USERID", userID);
                    return await conn.QueryAsync<GroupViewModel>(SPConstants.getGroupsUserGroups, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Create Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<bool> Create(GroupViewModel group)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@GROUPTYPEID", group.GROUPTYPEID);
                    param.Add("@GROUPNAME ", group.GROUPNAME);
                    param.Add("@LANGUAGEID ", group.LANGUAGEID);
                    param.Add("@COMPANYID", group.COMPANYID);
                    param.Add("@STATUS", group.STATUS);
                    param.Add("@CREATEDBY", group.CREATEDBY);
                    await conn.ExecuteAsync(SPConstants.CreateGroup, param, commandType: CommandType.StoredProcedure);
                    ////Something doing here (Fetch Integer ID)
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Group Assignment
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<bool> UpdateGroupAssignment(GroupViewModel group)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@GROUPTYPEID", group.GROUPTYPEID);
                    param.Add("@GROUPID", group.GROUPID);
                    param.Add("@UPDATEDBY", group.UPDATEDBY);
                    param.Add("@USERID", group.USERID);
                    await conn.ExecuteAsync(SPConstants.UpdateGroupAssignment, param, commandType: CommandType.StoredProcedure);
                    ////Something doing here (Fetch Integer ID)
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Change Group Status
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<bool> ChangeGroupStatus(GroupViewModel group)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@GROUPTYPEID", group.GROUPTYPEID);
                    param.Add("@UPDATEDBY", group.UPDATEDBY);
                    param.Add("@GROUPID", group.GROUPID);                    
                    await conn.ExecuteAsync(SPConstants.ChangeGroupStatus, param, commandType: CommandType.StoredProcedure);
                    ////Something doing here (Fetch Integer ID)
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
