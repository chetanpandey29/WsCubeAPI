using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WS_Cube.Factory;

namespace WS_CubeAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private ISiteService _siteService;
        ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Controller Construction
        /// </summary>
        /// <param name="siteService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public SiteController(ISiteService siteService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _siteService = siteService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        #region Site
        /// <summary>
        /// Get Sites
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost("{userID}")]
        public async Task<IActionResult> GetSites(int userID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _siteService.GetSites(userID);
                return Ok(user);               
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        #endregion
    }
}