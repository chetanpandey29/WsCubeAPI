using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.ViewModel;

namespace WS_Cube.Factory
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupViewModel>> GetGroupList(int languageCode, int groupTypeID);
        Task<IEnumerable<GroupViewModel>> GetGroupTypeWithMandatory(int languageID);
        Task<IEnumerable<GroupViewModel>> GetGroupsUserGroups(int languageID, int userID);

        Task<bool> Create(GroupViewModel group);

        Task<bool> UpdateGroupAssignment(GroupViewModel group);

        Task<bool> ChangeGroupStatus(GroupViewModel group);

    }
}
