using System;
using System.Collections.Generic;
using System.Text;

namespace WS_Cube.Repository.Constants
{
    public class SPConstants
    {
        public const string getUsers = "GETUSERINFOBYUSERNAMEANDPASSWORD";

        public const string getUsersByID = "getUsersByID";       

        public const string getUserinfoByID = "GETUSERINFO_BYID";

        public const string deleteUserInfo = "DELETEUSER_1";

        public const string getUserInfo = "GETUSERINFO_1";

        public const string insertUser = "INSERTUSER_1";

        public const string getMainCategory = "GETMAINCATEGORY_1";

        public const string getRoles = "GETROLES_1";

        public const string getSites = "GETSITELISTING_1";

        public const string getAreaList = "GETAREALIST_1";

        public const string getLanguage = "GETACTIVELANGUAGES_1";

        public const string getGrouplist = "GETGROUPLIST_1";

        public const string  getGroupTypewithMandatory= "GETGROUPTYPEWITHMANDATOTY_1";

        public const string getGroupsUserGroups = "GETGROUPS_USERGROUPS_1";

        public const string CreateUser = "INSERTUSERNew";

        public const string UpdateUser = "UPDATEUSER_1";

        public const string CreateGroup = "INSERTGROUPS_1";        

        public const string UpdateGroupAssignment = "ASSIGNUSRSTOGRP_1";

        public const string ChangeGroupStatus = "CHANGESTATUS_GROUPS_1";

        public const string AddRole = "INSERTROLES_1";

        public const string DeleteRole = "DELETEROLES_1";

        public const string UpdateRole = "UPDATEROLES_1";

        public const string UpdateRoleStatus = "UPDATEROLESSTATUS_1";

        public const string GetRolePermission = "GETROLEPERMISSIONALL_1";

        public const string GetUserRoleAssignment = "GETUSERSLISTROLES_1";

        public const string AddTargetMultiUser = "INSERTTARGETMULTIUSERS_1";        

    }
}
