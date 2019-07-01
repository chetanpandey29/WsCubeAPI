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
    public class SiteService : ISiteService
    {
        private ISiteRepository _siteRepository;
        ILoggerFactory _loggerFactory;
        ILogger _logger;

        public SiteService(ISiteRepository siteRepository, ILoggerFactory loggerFactory)
        {
            this._siteRepository = siteRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }

        /// <summary>
        /// Get Sites
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SiteViewModel>> GetSites(int userID)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            if (string.IsNullOrEmpty(userID.ToString()))
                return null;

            var user = await _siteRepository.GetSites(userID);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }
    }
}
