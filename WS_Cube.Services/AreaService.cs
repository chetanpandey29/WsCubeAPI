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
    public class AreaService : IAreaService
    {
        private IAreaRepository _areaRepository;
        ILoggerFactory _loggerFactory;
        ILogger _logger;
        public AreaService(IAreaRepository areaRepository, ILoggerFactory loggerFactory)
        {
            this._areaRepository = areaRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger(typeof(UserService));
        }
        /// <summary>
        /// Get Area List
        /// </summary>
        /// <param name="areaType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AreaViewModel>> GetAreaList(int areaType)
        {
            var logger = _loggerFactory.CreateLogger("LoggerCategory");
            if (string.IsNullOrEmpty(areaType.ToString()))
                return null;

            var user = await _areaRepository.GetAreaList(areaType);
            if (user == null)
            {
                logger.LogDebug(null);
                return null;
            }
            return user;
        }
    }
}
