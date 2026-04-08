using Microsoft.AspNetCore.Mvc;
using SmartStay.Domain.Interfaces;

namespace SmartStay.API.Controllers;

[ApiController]
[Route("[authentication]")]
public class AuthenticationController : ControllerBase
{
   // private readonly 
   // [HttpPost("login")]
   // public async Task Login([FromBody] string email, [FromBody] string password)//M.G: void for now
   // {
   //    
   // }
}