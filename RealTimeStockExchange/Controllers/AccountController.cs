using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;

namespace RealTimeStockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : _BaseController<ApplicationUser, AuthDTO>
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService) : base(authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<AuthDTO> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return new AuthDTO { };
            }

            var result = await _authService.LoginAsync(loginDTO);

            return result;
        }
    }
}
