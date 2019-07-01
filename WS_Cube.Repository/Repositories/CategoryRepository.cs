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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string connectionString;

        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ServerConnection");
            _configuration = configuration;
        }

        /// <summary>
        /// Get Categories
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryViewModel>> GetMainCategory(int languageID)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@LANGUAGEID", languageID);
                    param.Add("@MODE", null);
                    param.Add("@CORRACTIONID", null);
                    return await conn.QueryAsync<CategoryViewModel>(SPConstants.getMainCategory, param, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
