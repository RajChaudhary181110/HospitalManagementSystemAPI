using Microsoft.AspNetCore.Identity.Data;
using MultipleAction_API.Models;

namespace MultipleAction_API.Interface
{
    public interface ILoginSignUpService
    {
        Task<Patient> AuthLogin(string email,string password, string Flag);
        Task<StatusModell> UserRegistration(UserRegisterModel data, string Flag );

    }
}
