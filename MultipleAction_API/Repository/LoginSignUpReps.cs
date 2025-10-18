using Dapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Data.SqlClient;
using MultipleAction_API.DBContext;
using MultipleAction_API.Interface;
using MultipleAction_API.Models;
using System;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MultipleAction_API.Repository
{
    public class LoginSignUpReps : ILoginSignUpService
    {

        private readonly DapperDBContext _dBCon;

        public LoginSignUpReps(DapperDBContext dbContext)
        {
            _dBCon = dbContext;
        }

        public async Task<Patient?> AuthLogin(string email, string password,string Flag)
        {
            using (var connection = _dBCon.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", Flag);
                parameters.Add("@Email", email);
                parameters.Add("@Password", password);
                var res = await connection.QueryFirstOrDefaultAsync<Patient>(
                    "HMS_PatiantRegister_SP", parameters, commandType: CommandType.StoredProcedure
                    );
                return res;
            }
        }
        public async Task<StatusModell?> UserRegistration(UserRegisterModel data,string Flag)
        {
            using (var connection = _dBCon.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", Flag);
                parameters.Add("@Name", data.Name);
                parameters.Add("@Email", data.Email);
                parameters.Add("@Mobile", data.Mobile);
                parameters.Add("@Password", data.Password);
                //parameters.Add("@Password", HashPassword(model.Password));
                var HashPass = HashPassword(data.Password);
                //await connection.ExecuteAsync("MA_UserRegistration_SP", parameters, commandType: CommandType.StoredProcedure);
                var res = await connection.QueryFirstOrDefaultAsync<StatusModell>(
                   "MA_UserRegistration_SP", parameters, commandType: CommandType.StoredProcedure
                   ); 
                return res;

            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes); ;
            }
        }
    }
}
