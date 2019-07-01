using System;
using System.Collections.Generic;
using System.Text;

namespace WS_Cube.ViewModel
{
    public class UserInfoModel
    {
        public string USERNAME { get; set; }
        public string FIRSTNAME { get; set; }       
        public string LASTNAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public int USERTYPEID  { get; set; }
        public int ROLEID  { get; set; }
        public int COMPANYID { get; set; }
        public int SITEID { get; set; }
        public int GROUPID { get; set; }
        public int DEFAULTROLEID { get; set; }
        public string APPROVEOFFLINE { get; set; }
        public string JOBTITLE { get; set; }
        public int LANGUAGEID { get; set; }
        public int DEFAULTSITEID { get; set; }
        public bool OFFLINEACCESS { get; set; }
        public bool STATUS{ get; set; }
        public string DATEFORMAT{ get; set; }
        public int LOGGEDUSERID { get; set; }
    }
}
