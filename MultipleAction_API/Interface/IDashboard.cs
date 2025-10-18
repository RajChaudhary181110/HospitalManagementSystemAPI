using MultipleAction_API.Models;

namespace MultipleAction_API.Interface
{
    public interface IDashboard
    {
        Task<List<Patient>> GetAllUsers( string Flag);
    }
}
