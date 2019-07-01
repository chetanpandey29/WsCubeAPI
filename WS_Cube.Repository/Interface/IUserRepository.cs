using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.ViewModel;

namespace WS_Cube.Repository.Interface
{
    public interface IUserRepository
    {
        Task<UserListViewModel> GetUsers(string userName, string password);

        Task<UserListViewModel> GetById(int userId);

        Task<UserListViewModel> GetAllUsers();

        Task<UserListViewModel> GetUserInfoByID(int userID);

        Task<UserInfoModel> DeleteUserInfo(int userID, string updatedBy);

        Task<UserInfoModel> GetUserInfo(int userID);

        Task<UserInfoModel> GetUsers(int userID, int loggedUserID);

        Task<int> Create(UserListViewModel user);

        Task<int> Update(UserListViewModel user);

        Task<bool> AddTargetMultiUser(UserListViewModel user);

    }
}
