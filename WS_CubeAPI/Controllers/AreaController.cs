using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WS_Cube.Factory;

namespace WS_CubeAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private IAreaService _areaService;
        ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Controller construction
        /// </summary>
        /// <param name="areaService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public AreaController(IAreaService areaService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _areaService = areaService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        #region Area
        /// <summary>
        /// Get Area List
        /// </summary>
        /// <param name="areaType"></param>
        /// <returns></returns>
        [HttpPost("{areaType}")]
        public async Task<IActionResult> GetAreaList(int areaType)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _areaService.GetAreaList(areaType);
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