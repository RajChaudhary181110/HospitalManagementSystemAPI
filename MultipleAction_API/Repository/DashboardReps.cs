using Dapper;
using MultipleAction_API.DBContext;
using MultipleAction_API.Interface;
using MultipleAction_API.Models;
using System.Data;

namespace MultipleAction_API.Repository
{
    public class DashboardReps:IDashboard
    {

        private readonly DapperDBContext _dBCon;
        public DashboardReps(DapperDBContext dbContext)
        {
            _dBCon = dbContext;
        }
        public async Task<List<Patient?>> GetAllUsers( string Flag)
        {
            using (var connection = _dBCon.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", Flag);
                var res = await connection.QueryAsync<Patient?>(
                    "MA_UserRegistration_SP", parameters, commandType: CommandType.StoredProcedure
                    );
                return res.ToList();
            }
        }
    }
}
