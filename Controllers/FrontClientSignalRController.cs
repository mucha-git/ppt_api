using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Accounts;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    //[Authorize(Role.Hurt_admin, Role.Hurt_kontrahent, Role.Hurt_spedycja)]
    [Route("[controller]")]
    public class FrontClientSignalRController : BaseController
    {
        private readonly IFrontClientSignalRService _frontClientSignalRService;

        public FrontClientSignalRController(
            IFrontClientSignalRService frontClientSignalRService)
        {
            _frontClientSignalRService = frontClientSignalRService;
        }

        //[Authorize]
        [HttpGet("UpdateTradesForAccount")]
        public async Task<ActionResult> UpdateTradesForAccount()
        {
            await _frontClientSignalRService.UpdateTradesForAccount(Account.Id);
            return Ok(new { message = "Trejdy zostały zaktualizowane u klienta." });
        }

        [Authorize]
        [HttpGet("ConnectedUsers")]
        public async Task<ActionResult<ICollection<AccountResponse>>> ConnectedUsers()
        {
            var users = await _frontClientSignalRService.GetAllConnectedUsers();
            return Ok(users);
            //return Ok(new { message = "Awizacja zostało zaktualizowana u klienta.", awizacja });
        }
    }
}