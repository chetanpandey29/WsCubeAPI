using System.Collections.Generic;
using System.Threading.Tasks;
using WS_Cube.ViewModel;

namespace WS_Cube.Factory
{
    public interface IUserService
    {
        Task<UserListViewModel> GetUserByUserPassword(string userName, string password);

        Task<UserListViewModel> GetById(int userId);

        Task<UserListViewModel> GetAllUsers();

        Task<UserListViewModel> GetUserInfoByID(int userID);

        Task<UserInfoModel> DeleteUserInfo(int userID, string updatedBy);

        Task<UserInfoModel> GetUserInfo(int userID);

        Task<UserInfoModel> GetUsers(int userID, int loggedUserID);

        Task<bool> Create(UserListViewModel user);

        Task<bool> Update(UserListViewModel user);

        Task<bool> AddTargetMultiUser(UserListViewModel user);


    }
}
