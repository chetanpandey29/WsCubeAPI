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
    public class LanguageService : ILanguageService
    {
        private ILanguageRepository _languageRepository;
        ILoggerFactory _loggerFactory;
        ILogger _logger;
        public LanguageService(ILanguageRepository languageRepository, ILoggerFactory loggerFactory)
        {
            this._languageRepository = languageRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }
        /// <summary>
        /// Get Languages
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LanguageViewModel>> GetLanguages()
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            var user = await _languageRepository.GetLanguages();
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }
    }
}
