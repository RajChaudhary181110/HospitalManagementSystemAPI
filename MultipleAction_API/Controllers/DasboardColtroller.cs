using Microsoft.AspNetCore.Mvc;
using MultipleAction_API.Interface;
using MultipleAction_API.Repository;

namespace MultipleAction_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DasboardColtroller : Controller
    {

        private IDashboard _InterfaceCon;
        private readonly IConfiguration _config;
        public DasboardColtroller(IDashboard Dashboard, IConfiguration configration)
        {
            _InterfaceCon = Dashboard;
            _config = configration;
        }

        [HttpGet]
        [Route("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            var result = await _InterfaceCon.GetAllUsers("GetAllUsers");

            return Ok(new { User = result });
        }
    }
}
