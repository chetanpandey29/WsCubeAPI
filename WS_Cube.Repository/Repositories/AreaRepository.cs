using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.Repository.Constants;
using WS_Cube.Repository.Interface;
using WS_Cube.ViewModel;

namespace WS_Cube.Repository.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;

        public AreaRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
        }

        /// <summary>
        /// Get Area List
        /// </summary>
        /// <param name="areaType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AreaViewModel>> GetAreaList(int areaType)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@AREATYPE", areaType);
                    param.Add("@USERID", null);
                    return await conn.QueryAsync<AreaViewModel>(SPConstants.getAreaList, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

