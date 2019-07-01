using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.Factory;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        ILoggerFactory _loggerFactory;
        ILogger _logger;
        public CategoryService(ICategoryRepository categoryRepository, ILoggerFactory loggerFactory)
        {
            this._categoryRepository = categoryRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }

        /// <summary>
        /// Get Main Category
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryViewModel>> GetMainCategory(int languageID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            if (string.IsNullOrEmpty(languageID.ToString()))
                return null;

            var user = await _categoryRepository.GetMainCategory(languageID);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }
    }
}
