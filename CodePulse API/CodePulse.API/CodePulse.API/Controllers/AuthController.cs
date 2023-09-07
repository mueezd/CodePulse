using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        //POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            //Check User Name

            var identityUser =  await _userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                //Check Password
                var checkPasswordResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    //Chreate A token and Response

                    var JwtToken = _tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = JwtToken
                    };

                    return Ok(response);
                }
            }

            ModelState.AddModelError("", "Email Or Password Incorrect");

            return ValidationProblem(ModelState);
        }

        //POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            //Create The Identity User Onject

            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
            };

            var identityRsult = await _userManager.CreateAsync(user, request.Password);

            if (identityRsult.Succeeded)
            {
                // Add role to Uuser (Reader)
                identityRsult = await _userManager.AddToRoleAsync(user, "Reader");

                if (identityRsult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityRsult.Errors.Any())
                    {
                        foreach (var error in identityRsult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityRsult.Errors.Any())
                {
                    foreach (var error in identityRsult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
            
        }
    }
}
