using Microsoft.AspNetCore.Mvc;
using pearlbox_api.business.DataTransferObjects;
using pearlbox_api.business.Services;

namespace pearlbox_api.web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost(Name = "Register")]
    public async Task<IActionResult> Register(UserDetails userDetails)
    {
        await authenticationService.RegisterUser(userDetails);
        return Ok();
    }
}
