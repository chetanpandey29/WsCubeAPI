using System;
using System.Collections.Generic;
using System.Text;

namespace WS_Cube.ViewModel
{
    public class RoleViewModel
    {
        public int ROLEID { get; set; }
        public string ROLENAME { get; set; }
        public string COPYFROM { get; set; }
        public int COMPANYID { get; set; }
        public string DESCRIPTION { get; set; }
        public int STATUS { get; set; }
        public int CREATEDBY { get; set; }
        public int UPDATEDBY { get; set; }
        public int USERID { get; set; }
    }
}
