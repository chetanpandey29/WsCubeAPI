using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WS_Cube.Factory;
using WS_Cube.Repository.Constants;
using WS_Cube.ViewModel;

namespace WS_CubeAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        ILoggerFactory _loggerFactory;

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Controller Constructor
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public RoleController(IRoleService roleService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _roleService = roleService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        #region Role
        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost("{userID}")]
        public async Task<IActionResult> GetRoles(int userID)
        { 
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _roleService.GetRoles(userID);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        public static Dictionary<string, object> scriptengine_Rdata;

        private IHostingEnvironment _hostingEnvironment;

        public string finalquery;
        private static string GetConnectionString()
        {
            return "Data Source=DESKTOP-8PPKK3C; Integrated Security=true; Database=Wscube_DB;" +
             "Initial Catalog=Wscube_DB; ";
        }
        
        [HttpPost("value/{tmpname}")]
        public string GetValue(string tmpname)
        {
            scriptengine_Rdata = null;
            if (scriptengine_Rdata == null)
            {
                //StreamReader sr = File.OpenText(HttpContext.Current.Server.MapPath("~/APPMOD/CONFIG/MASTERS1.1.config"));

                SqlConnection connection = new SqlConnection(GetConnectionString());

                var path = Path.Combine(_hostingEnvironment.WebRootPath, "E:/IP Data/SQL/GETUSERROLES_1.0.config");

                //string strSettings = sr.ReadToEnd();
                //scriptengine_Rdata = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(strSettings);
                //sr.Close();
                Dictionary<string, object> tmpdata = new Dictionary<string, object>();
                tmpdata = scriptengine_Rdata;

                string str = tmpdata.FirstOrDefault(x => x.Key == tmpname).Value.ToString();
                string[] queries = str.Split('-');
                string[] finalqueries = new string[queries.Length];
                for (var k = 0; k <= queries.Length - 1; k++)
                {
                    string aa = "~/APPMOD/CONFIG/" + queries[k] + ".config";
                    //StreamReader sr1 = File.OpenText(HttpContext.Current.Server.MapPath("~/APPMOD/SQL/" + queries[k] + ".config"));

                    //finalqueries[k] = sr1.ReadToEnd();
                    //sr.Close();
                }
                finalquery = string.Join(";", finalqueries).ToString();
            }
            return finalquery;
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("deleterole")]
        public IActionResult DeleteRole([FromBody]RoleViewModel role)
        {
            try
            {
                _roleService.DeleteRole(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update Roles
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("updaterole")]
        public IActionResult UpdateRole([FromBody]RoleViewModel role)
        {
            try
            {
                _roleService.UpdateRole(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update Role Status
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("updaterolestatus")]
        public IActionResult UpdateRoleStatus([FromBody]RoleViewModel role)
        {
            try
            {
                _roleService.UpdateRoleStatus(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get Role Permission
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("getrolepermission")]
        public IActionResult GetRolePermission([FromBody]RoleViewModel role)
        {
            try
            {
                _roleService.GetRolePermission(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get UserRole Assignment
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("getuserroleassignment")]
        public IActionResult GetUserRoleAssignment([FromBody]RoleViewModel role)
        {
            try
            {
                _roleService.GetUserRoleAssignment(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion
    }
}