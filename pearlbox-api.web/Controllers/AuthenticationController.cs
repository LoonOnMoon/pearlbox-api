using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using pearlbox_api.business.DataTransferObjects;
using pearlbox_api.business.Services;
using pearlbox_api.business.Exceptions.Authentication;
using pearlbox_api.web.Models.FormModels.Authentication;

namespace pearlbox_api.web.Controllers;

[ApiController]
[Route("/api/Auth/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAuthenticationService authenticationService;
    public AuthenticationController(
        IMapper mapper,
        IAuthenticationService authenticationService
        )
    {
        this.authenticationService = authenticationService;
        this.mapper = mapper;
    }

    [HttpPost(Name = "SignUp")]
    public async Task<IActionResult> SignUp([FromBody]SignUpFormModel formModel)
    {
        if (!ModelState.IsValid)
            return Unauthorized(ModelState.Values);
        try
        {
            var userDetails = mapper.Map<SignUpFormModel, SignUpUserDetails>(formModel);
            var token = await authenticationService.SignUpUser(userDetails);
            
            return Ok(new
            {
                token = token
            });
        }
        catch (Exception e) when (e is SignUpFailedException || e is SignUpUserExistsException)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost(Name = "SignIn")]
    public async Task<IActionResult> SignIn([FromBody]SignInWithPasswordFormModel formModel)
    {
        if (!ModelState.IsValid)
            return Unauthorized(ModelState.Values);
        try
        {
            var userDetails = mapper.Map<SignInWithPasswordFormModel, SignInWithPasswordUserDetails>(formModel);
            var result = await authenticationService.SignInUser(userDetails);
            
            return Ok(new
            {
                token = result
            });
        }
        catch (Exception e) when (e is SignInIncorrectPasswordException || e is SignInUserDoesNotExistException)
        {
            return Unauthorized("Incorrect username/email or password");
        }
        
    }
    [Authorize]
    [HttpGet(Name = "AuthTest")]
    public bool AuthTest()
    {
        return true;
    }
}
