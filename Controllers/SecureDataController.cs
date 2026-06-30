using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SecureDataController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSecureData()
    {
        var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

        return Ok(new {Message = $"Congratulations {userName}, you have reached a protected endpoint!" });
        
    }
}
