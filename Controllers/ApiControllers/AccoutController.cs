using APIWeb1.Dtos.Account;
using APIWeb1.Extensions;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<AppUser> userManager, ITokenRepository tokenRepository, SignInManager<AppUser> signInManager, IConfiguration configuration,IEmailSender emailSender)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _signinManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            
            if (user == null || user.Status==0) return Unauthorized("Account does not exist!");
            if ( user.Status == 2) return Unauthorized("Account has been locked, or not confirmed!");
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

        [HttpPost("change-pass")]
        [Authorize]
        public async Task<IActionResult> ChangePass([FromBody] ChangePass model)
        {
            var username = User.GetUsername();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurentPass, model.NewPass);
            if (!result.Succeeded)
            {
                // Trả về lỗi nếu mật khẩu không thỏa mãn
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok("Password changed successfully");

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

                user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == registerEmployerDto.Username.ToLower().Trim());
                if (user != null) return Unauthorized("The username you set is duplicated!");

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

        // API kiểm tra token
        [HttpPost("checktoken")]
        public IActionResult CheckToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Token is missing");

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]); // Lấy key từ appsettings
                var tokenValidationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Issuer"], // Lấy Issuer từ appsettings
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:Audience"], // Lấy Audience từ appsettings
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var validatedToken);

                // Trích xuất thông tin từ token
                var username = principal.FindFirstValue(ClaimTypes.GivenName); // Đảm bảo trích xuất đúng claim
                var email = principal.FindFirstValue(ClaimTypes.Email);
                // Lấy tất cả các roles
                var roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

                return Ok(new
                {
                    message = "Token is valid",
                    user = new
                    {
                        username = username,
                        email = email,
                        roles = roles
                    }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid token: " + ex.Message);
            }
        }

        [HttpPost("Forget-pass")]
        public async Task<IActionResult> ForgetPass([FromBody] string username)
        {
            // Tạo số ngẫu nhiên gồm 6 chữ số
            var random = new Random();
            int otp = random.Next(100000, 999999); // Tạo số ngẫu nhiên từ 100000 đến 999999

            // Tạo chữ cái viết hoa ngẫu nhiên
            var uppercaseLetter = (char)random.Next('A', 'Z' + 1);

            // Tạo chữ cái viết thường ngẫu nhiên
            var lowercaseLetter = (char)random.Next('a', 'z' + 1);

            // Kết hợp OTP và các chữ cái vào mật khẩu
            string newPassword = $"{otp}{uppercaseLetter}{lowercaseLetter}";

            // Tìm người dùng theo username
            var userInfo = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (userInfo == null)
            {
                return BadRequest("Username is invalid");
            }

            // Tạo token đặt lại mật khẩu
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(userInfo);

            // Đặt lại mật khẩu với OTP làm mật khẩu mới
            var resetPasswordResult = await _userManager.ResetPasswordAsync(userInfo, resetToken, newPassword);

            if (!resetPasswordResult.Succeeded)
            {
                // Trả về lỗi nếu việc đặt lại mật khẩu không thành công
                return BadRequest("Failed to reset password");
            }

            // Nội dung email
            string subject = "Web Job Update Notification";
            string htmlMessage = $"<p>Your new password is <strong>{newPassword}</strong></p>";

            // Gửi email thông báo mật khẩu mới
            await _emailSender.SendEmailAsync(userInfo.Email, subject, htmlMessage);

            return Ok("Password reset successfully. Please check your email for the new password.");
        }



    }
}
