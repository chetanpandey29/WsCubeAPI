using System;
using System.Collections.Generic;
using System.Text;

namespace WS_Cube.ViewModel
{
    public class GroupViewModel
    {
        public int GROUPID { get; set; }
        public string GROUPNAME { get; set; }
        public string STATUS { get; set; }
        public int GROUPTYPEID { get; set; }
        public string GROUPTYPENAME { get; set; }
        public string MANDATORY { get; set; }
        public bool FLAG { get; set; }
        public int LANGUAGEID{ get; set; }
        public int COMPANYID { get; set; }
        public string CREATEDBY { get; set; }
        public int UPDATEDBY { get; set; }
        public int USERID { get; set; }
    }
}
