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
    public class LanguageRepository : ILanguageRepository
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;

        public LanguageRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
        }

        /// <summary>
        /// Get Languages
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LanguageViewModel>> GetLanguages()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    return await conn.QueryAsync<LanguageViewModel>(SPConstants.getLanguage, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
