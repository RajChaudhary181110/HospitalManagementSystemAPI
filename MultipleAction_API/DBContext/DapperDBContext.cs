using Microsoft.Data.SqlClient;
using System.Data;

namespace MultipleAction_API.DBContext
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;

        public DapperDBContext (IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("Dbconnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionstring);
    }
}
