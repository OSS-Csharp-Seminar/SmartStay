// using Microsoft.AspNetCore.Mvc;
// using SmartStay.Application;
// using SmartStay.Application.Dto.UserDto;
//
// namespace SmartStay.API.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class AuthenticationController : ControllerBase
// {
//    private readonly IAuthenticationService _authenticationService;
//    
//    public AuthenticationController(IAuthenticationService authenticationService)
//    {
//       _authenticationService = authenticationService;
//    }
//    
//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto dto)
//    {
//       var response = await _authenticationService.Authenticate(dto);
//       
//       HttpContext.Response.Cookies.Append("id",response.UserId.ToString(), new CookieOptions
//          {
//             Expires = DateTimeOffset.UtcNow.AddMinutes(120),//M.G: change if you have good reason.
//             HttpOnly = true,// M.G: prevents cookie steal with java XSS script
//             IsEssential = true
//          }
//          );
//       
//       return Ok();
//    }
// }