using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MultipleAction_API.DBContext;
using MultipleAction_API.Interface;
using MultipleAction_API.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace MultipleAction_API.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class LoginSignUp:ControllerBase
    {
        private ILoginSignUpService _InterfaceCon;
        private readonly IConfiguration _config;
        public LoginSignUp(ILoginSignUpService loginSingUpService, IConfiguration configration)
        {
            _InterfaceCon = loginSingUpService;
            _config = configration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAuth([FromBody] LoginRequest LoginId)
        {
            // Example: Get user by email from DB
            var user = await _InterfaceCon.AuthLogin(LoginId.Email, LoginId.Password, "PatientLogin");

            if (user != null)
            {
                var Claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("PatientNumber",user.PatientNumber.ToString()),
                    new Claim("Email",user.Email.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
                var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    Claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                );
                string tokenValue=new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token=tokenValue,User=user });
            }
            return NoContent();
        }        

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegisterModel data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var result = await _InterfaceCon.UserRegistration(data, "UserRegister");

            return Ok(new { Message = result.Msg, Status = result.Status });
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginRequest data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            var result = await _InterfaceCon.AuthLogin(data.Email, data.Password, "ForgotPassword");

            return Ok(new { User = result });
        }
    }
}
