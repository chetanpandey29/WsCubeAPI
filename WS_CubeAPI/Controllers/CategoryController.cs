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
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        ILoggerFactory _loggerFactory;
        public IConfiguration Configuration { get; }

        #region Category
        /// <summary>
        /// Category controller
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="configuration"></param>
        /// <param name="loggerFactory"></param>
        public CategoryController(ICategoryService categoryService, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _categoryService = categoryService;
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Get Main Category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpPost("{categoryID}")]
        public async Task<IActionResult> GetMainCategory(int categoryID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            try
            {
                var user = await _categoryService.GetMainCategory(categoryID);
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