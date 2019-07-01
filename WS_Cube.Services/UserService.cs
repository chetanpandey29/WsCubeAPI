using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS_Cube.Factory;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        ILoggerFactory _loggerFactory;
        ILogger _logger;

        public UserService(IUserRepository userRepository, ILoggerFactory loggerFactory)
        {
            this._userRepository = userRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }

        /// <summary>
        /// Get User and Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserListViewModel> GetUserByUserPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userRepository.GetUsers(username, password);
            if (user == null)
                return null;
            return user;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Create(UserListViewModel user)
        {
            if (user == null)
                return false;

            var flag = await _userRepository.Create(user);
            return true;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        public async Task<UserListViewModel> GetAllUsers()
        {

            var user = await _userRepository.GetAllUsers();
            return user;
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserListViewModel> GetById(int userId)
        {
            if (string.IsNullOrEmpty(userId.ToString()))
                return null;
            var user = await _userRepository.GetById(userId);
            if (user == null)
                return null;
            return user;
        }
        
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserListViewModel> GetUserInfoByID(int userId)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            if (string.IsNullOrEmpty(userId.ToString()))
                return null;

            var user = await _userRepository.GetUserInfoByID(userId);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            logger.LogDebug(string.IsNullOrEmpty(user.EmailId) ? string.Empty : user.EmailId);
            return user;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<UserInfoModel> DeleteUserInfo(int userID, string updatedBy)
        {
            if (string.IsNullOrEmpty(userID.ToString()))
                return null;
            var user = await _userRepository.DeleteUserInfo(userID, updatedBy);
            if (user == null)
                return null;
            return user;
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<UserInfoModel> GetUserInfo(int userID)
        {
            _logger.LogDebug("Started");
            if (string.IsNullOrEmpty(Convert.ToString(userID)))
            {
                _logger.LogDebug(null);
                return null;
            }
            var user = await _userRepository.GetUserInfo(userID);
            if (user == null)
            {
                _logger.LogDebug(null);
                return null;
            }
            return user;
        }

        public async Task<UserInfoModel> GetUsers(int userID, int loggedUserID)
        {
            _logger.LogDebug("Started");
            if (string.IsNullOrEmpty(Convert.ToString(userID)))
            {
                _logger.LogDebug(null);
                return null;
            }
            var user = await _userRepository.GetUsers(userID, loggedUserID);
            if (user == null)
            {
                _logger.LogDebug(null);
                return null;
            }
            return user;
        }

        

        // private helper methods

        /// <summary>
        /// Create Password Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verify Password Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
               
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Update(UserListViewModel user)
        {
            if (user == null)
                return false;

            var flag = await _userRepository.Update(user);
            return true;
        }

        /// <summary>
        /// Add Target MultiUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddTargetMultiUser(UserListViewModel user)
        {
            if (user == null)
                return false;

            var flag = await _userRepository.AddTargetMultiUser(user);
            return true;
        }
    }
}
