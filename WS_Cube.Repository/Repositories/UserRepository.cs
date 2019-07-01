using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WS_Cube.Repository.Constants;
using WS_Cube.Repository.Infrastructure;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Repository.Repositories
{
    public class Userepository : IUserRepository
    {
        private readonly string connectionString;
        private readonly IConfiguration _configuration;
        private readonly IScriptEngine _scriptEngine;

        string strquery = string.Empty;

        public Userepository(IConfiguration configuration, IScriptEngine scriptEngine)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
            _scriptEngine = scriptEngine;
        }

        /// <summary>
        /// To get Operations
        /// </summary>
        /// <returns></returns>
        public async Task<UserListViewModel> GetUsers(string userName, string password)
        {            
            strquery = _scriptEngine.GetValue("LOGIN");

            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@EMAIL", userName);
                param.Add("@USERNAME", userName);
                param.Add("@PASSWORD", password);
                return await conn.QuerySingleAsync<UserListViewModel>(strquery, param, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public async Task<UserListViewModel> GetAllUsers()
        {

            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                return await conn.QuerySingleAsync<UserListViewModel>(SPConstants.getUsers, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserListViewModel> GetById(int userId)
        {
            strquery = _scriptEngine.GetValue("GETUSERINFO");
            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                return await conn.QuerySingleAsync<UserListViewModel>(strquery, param, commandType: CommandType.Text);
            }

        }

        /// <summary>
        /// Get User Info from ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserListViewModel> GetUserInfoByID(int userId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                return await conn.QuerySingleAsync<UserListViewModel>(SPConstants.getUserinfoByID, param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<UserInfoModel> DeleteUserInfo(int userID, string updatedBy)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userID);
                param.Add("@UpdatedBy", updatedBy);
                return await conn.QuerySingleAsync<UserInfoModel>(SPConstants.deleteUserInfo, param, commandType: CommandType.StoredProcedure);

            }

        }

        /// <summary>
        /// Get USer info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoModel> GetUserInfo(int userId)
        {
            strquery = _scriptEngine.GetValue("GETUSERINFO - Copy");
            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                param.Add("@LOGGEDUSERID", userId);
                return await conn.QuerySingleAsync<UserInfoModel>(strquery, param, commandType: CommandType.Text);
            }
        }

        public async Task<UserInfoModel> GetUsers(int userID, int loggedUserID)
        {
            strquery = _scriptEngine.GetValue("GETUSERINFO");
            using (var conn = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userID);
                param.Add("@LOGGEDUSERID", loggedUserID);
                return await conn.QuerySingleAsync<UserInfoModel>(strquery, param, commandType: CommandType.Text);
            }

        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> Create(UserListViewModel user)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserName", user.UserName);
                    param.Add("@FIRSTNAME ", user.FirstName);
                    param.Add("@LASTNAME ", user.LastName);
                    param.Add("@EMAIL ", user.EmailId);
                    param.Add("@PASSWORD ", user.Password);
                    param.Add("@CREATEDBY", user.CreatedBy);
                    param.Add("@USERTYPEID ", user.usertypeid);
                    param.Add("@ROLEID ", user.roleid);
                    param.Add("@COMPANYID", user.companyid);
                    param.Add("@SITEID ", user.siteid);
                    param.Add("@GROUPID ", user.groupid);
                    param.Add("@DEFAULTROLEID ", user.defaultroleid);
                    param.Add("@APPROVEOFFLINE", user.approveoffline);
                    param.Add("@JOBTITLE ", user.jobtitle);
                    param.Add("@LANGUAGEID ", user.languageid);
                    param.Add("@DEFAULTSITEID ", user.defaultsiteid);
                    param.Add("@OFFLINEACCESS ", user.offlineaccess);
                    param.Add("@status ", user.status);
                    param.Add("@DATEFORMAT ", user.dateformat);
                    return await conn.ExecuteAsync(SPConstants.CreateUser, param, commandType: CommandType.StoredProcedure);
                }                
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> Update(UserListViewModel user)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@USERID", user.UserId);
                    param.Add("@PASSWORD", user.Password);
                    param.Add("@USERTYPEID", user.usertypeid);
                    param.Add("@UPDATEDBY", user.UPDATEDBY);
                    param.Add("@USERNAME", user.UserName);
                    param.Add("@FIRSTNAME", user.FirstName);
                    param.Add("@LASTNAME", user.LastName);
                    param.Add("@STATUS", user.status);
                    param.Add("@COMPANYID", user.companyid);
                    param.Add("@DEFAULTSITEID", user.defaultsiteid);
                    param.Add("@DEFAULTROLEID", user.defaultroleid);
                    param.Add("@EMAIL", user.EmailId);
                    param.Add("@JOBTITLE", user.jobtitle);
                    param.Add("@LANGUAGEID", user.languageid);
                    param.Add("@DATEFORMAT", user.dateformat);
                    param.Add("@OFFLINEACCESS", user.offlineaccess);
                    param.Add("@APPROVEOFFLINE", user.approveoffline);
                    param.Add("@GROUPID", user.groupid);
                    param.Add("@ROLEID", user.roleid);
                    param.Add("@SITEID", user.siteid);
                    return await conn.ExecuteAsync(SPConstants.UpdateUser, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add Target MultiUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddTargetMultiUser(UserListViewModel user)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@USERID", user.UserId);
                    param.Add("@SITEID", user.siteid);
                    param.Add("@CREATEDBY", user.CreatedBy);
                    param.Add("@JAN", user.JAN);
                    param.Add("@FEB", user.FEB);
                    param.Add("@MAR", user.MAR);
                    param.Add("@APR", user.APR);
                    param.Add("@MAY", user.MAY);
                    param.Add("@JUN", user.JUN);
                    param.Add("@JUL", user.JUL);
                    param.Add("@AUG", user.AUG);
                    param.Add("@SEP", user.SEP);
                    param.Add("@OCT", user.OCT);
                    param.Add("@NOV", user.NOV);
                    param.Add("@DEC", user.DEC);
                    await conn.ExecuteAsync(SPConstants.AddTargetMultiUser, param, commandType: CommandType.StoredProcedure);
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
