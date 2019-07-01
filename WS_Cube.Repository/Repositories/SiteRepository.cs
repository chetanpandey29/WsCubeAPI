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
    public class SiteRepository : ISiteRepository
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;

        public SiteRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
        }

        /// <summary>
        /// Get Sites
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SiteViewModel>> GetSites(int userID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@USERID", userID);
                    return await conn.QueryAsync<SiteViewModel>(SPConstants.getSites, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

