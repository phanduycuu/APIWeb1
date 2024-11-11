using APIWeb1.Dtos.Account;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenRepository tokenRepository, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _signinManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            
            if (user == null || user.Status==1) return Unauthorized("Account does not exist!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenRepository.CreateToken(user)
                }
            );
        }

        [HttpPost("registeruser")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var user= await _userManager.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email.ToLower().Trim());
                if(user!=null) return Unauthorized("Invalid email!");

                user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == registerDto.Username.ToLower().Trim());
                if (user != null) return Unauthorized("The username you set is duplicated!");

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    Fullname=registerDto.Fullname,
                    Status=1

                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = await _tokenRepository.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPost("registeremployer")]
        public async Task<IActionResult> RegisterEmolyer([FromBody] RegisterEmployerDto registerEmployerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == registerEmployerDto.Email.ToLower());
                if (user != null) return Unauthorized("Invalid email!");
                var appUser = new AppUser
                {
                    UserName = registerEmployerDto.Username,
                    Email = registerEmployerDto.Email,
                    Fullname = registerEmployerDto.Fullname,
                    CompanyId = registerEmployerDto.CompanyId,
                    Status=0,

                };

                var createdUser = await _userManager.CreateAsync(appUser, registerEmployerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Employer");
                    if (roleResult.Succeeded)
                    {
                        return Ok();
                                                  
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
