using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WS_Cube.Factory;

namespace WS_CubeAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private ILanguageService _languageService;

        ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Controller construction
        /// </summary>
        /// <param name="languageService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public LanguageController(ILanguageService languageService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _languageService = languageService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        #region Languages
        /// <summary>
        /// Get Languages
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetLanguages()
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _languageService.GetLanguages();
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