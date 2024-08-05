using Castle.Core.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using MilkNest.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Security.Cryptography;

namespace MilkNest.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IEmailService emailService, ISmsService smsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
            _smsService = smsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme);
                var message = $"<p>Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a></p>";
                await _emailService.SendEmailAsync(user.Email, "Confirm your email", message);

                return Ok(new { message = "User created successfully. Please check your email to confirm your account." });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new { message = "Email confirmed successfully." });
            }

            return BadRequest(new { message = "Error confirming your email." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return StatusCode(StatusCodes.Status423Locked, "Account is locked. Try again later.");
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (await _userManager.GetTwoFactorEnabledAsync(user))
                {
                   
                    return Ok(new { message = "Two-factor authentication is enabled. Please provide your 2FA code.", twoFactorRequired = true });
                }
                else
                {
               
                    var token = GenerateJwtToken(user);
                    var refreshToken = GenerateRefreshToken();
                    await SaveRefreshToken(user, refreshToken);
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return Ok(new { token, refreshToken });
                }
            }

            if (result.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status423Locked, "Account is locked. Try again later.");
            }

            return Unauthorized();
        }
        [HttpPost("send-2fa-code")]
        public async Task<IActionResult> Send2FACode([FromBody] Send2FACodeModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

          
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

          
            await _smsService.SendSmsAsync(user.PhoneNumber, $"Your 2FA code is: {code}");

            return Ok(new { message = "2FA code has been sent." });
        }
        [Authorize]
        [HttpPost("enable-2fa-sms")]
        public async Task<IActionResult> Enable2FASms([FromBody] Enable2FAModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            await _smsService.SendSmsAsync(model.PhoneNumber, $"Your verification code is: {token}");

            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);

            return Ok(new { message = "Verification code sent to your phone." });
        }
        [Authorize]
        [HttpPost("verify-2fa-sms")]
        public async Task<IActionResult> Verify2FASms([FromBody] Verify2FAModel model)
        {
         
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var is2FATokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, model.Token);
            if (!is2FATokenValid)
            {
                return BadRequest("Invalid token");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
            return Ok("2FA has been enabled");
        }
        [HttpPost("login-2fa")]
        public async Task<IActionResult> LoginWith2FA([FromBody] Login2FAModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultEmailProvider, model.Token, isPersistent: true, rememberClient: true);

            if (result.Succeeded)
            {
                var jwtToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                await SaveRefreshToken(user, refreshToken);

                return Ok(new { token = jwtToken, refreshToken });
            }

            result = await _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultPhoneProvider, model.Token, isPersistent: true, rememberClient: true);

            if (result.Succeeded)
            {
                var jwtToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                await SaveRefreshToken(user, refreshToken);

                return Ok(new { token = jwtToken, refreshToken });
            }

            return Unauthorized();
        }
      
        private string GenerateJwtToken(ApplicationUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
         new Claim(JwtRegisteredClaimNames.Email, user.Email),
           new Claim(JwtRegisteredClaimNames.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task SaveRefreshToken(ApplicationUser user, string refreshToken)
        {
            var userToken = await _userManager.GetAuthenticationTokenAsync(user, "MilkNest", "refresh_token");
            if (userToken != null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "MilkNest", "refresh_token");
            }
            await _userManager.SetAuthenticationTokenAsync(user, "MilkNest", "refresh_token", refreshToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenModel model)
        {
            var principal = GetPrincipalFromExpiredToken(model.Token);
            if (principal == null)
            {
                return BadRequest(new { message = "Invalid token" });
            }

            var username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid token" });
            }

            var savedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MilkNest", "refresh_token");
            if (savedRefreshToken != model.RefreshToken)
            {
                return BadRequest(new { message = "Invalid refresh token" });
            }

            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await SaveRefreshToken(user, newRefreshToken);

            return Ok(new { token = newJwtToken, refreshToken = newRefreshToken });
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
      
        [AllowAnonymous]
        [HttpGet("external-login")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            ControllerContext.HttpContext.Session.Clear();
            return Challenge(properties, provider);
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _signInManager.SignOutAsync();
            await _userManager.RemoveAuthenticationTokenAsync(user, "MilkNest", "refresh_token");

            return Ok(new { message = "Logout successful" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("lockout")]
        public async Task<IActionResult> LockoutUser([FromBody] LockoutModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5)); 
            return Ok(new { message = "User locked out successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("unlock")]
        public async Task<IActionResult> UnlockUser([FromBody] UnlockModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _userManager.SetLockoutEndDateAsync(user, null);
            return Ok(new { message = "User unlocked successfully." });
        }
        [HttpPost("enable-2fa-email")]
        public async Task<IActionResult> Enable2FAEmail([FromBody] Enable2FAModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            var callbackUrl = Url.Action(nameof(Verify2FAEmail), "Account", new { code }, Request.Scheme);

            var message = $"<p>Your 2FA code is: {code}</p>";
            await _emailService.SendEmailAsync(user.Email, "Your 2FA Code", message);

            return Ok(new { message = "2FA code has been sent to your email." });
        }
        [HttpPost("verify-2fa-email")]
        public async Task<IActionResult> Verify2FAEmail([FromBody] Verify2FAModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var is2FATokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, model.Token);
            if (!is2FATokenValid)
            {
                return BadRequest("Invalid token");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return Ok("2FA has been enabled.");
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var externalLoginInfo = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return BadRequest(new { message = "Error loading external login information." });
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                await SaveRefreshToken(user, refreshToken);

                return Ok(new { token, refreshToken });
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = email,
                            Email = email,
                        };
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                            await _userManager.AddClaimAsync(user, new Claim("UserType", "External"));
                        }
                    }

                    var addLoginResult = await _userManager.AddLoginAsync(user, info);
                    if (addLoginResult.Succeeded)
                    {
                        await _userManager.SetAuthenticationTokenAsync(user, info.LoginProvider, "access_token", info.AuthenticationTokens.FirstOrDefault(t => t.Name == "access_token")?.Value);
                        await _userManager.SetAuthenticationTokenAsync(user, info.LoginProvider, "refresh_token", info.AuthenticationTokens.FirstOrDefault(t => t.Name == "refresh_token")?.Value);

                        await _signInManager.SignInAsync(user, isPersistent: true);
                        var token = GenerateJwtToken(user);
                        var refreshToken = GenerateRefreshToken();
                        await SaveRefreshToken(user, refreshToken);

                        return Ok(new { token, refreshToken });
                    }
                }

                return BadRequest(new { message = $"Email claim not received from: {info.LoginProvider}" });
            }

        }


        [HttpGet("LoginSuccess")]
        public IActionResult LoginSuccess()
        {
            return Ok(new { message = "Login Successful" });
        }

        [HttpGet("LoginFailure")]
        public IActionResult LoginFailure()
        {
            return BadRequest(new { message = "Login Failed" });
        }

        public class LockoutModel
        {
            public string UserId { get; set; }
        }

        public class UnlockModel
        {
            public string UserId { get; set; }
        }
        public class RegisterModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class Send2FACodeModel
        {
            public string Email { get; set; }
        }
        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class TokenModel
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }
        public class Login2FAModel
        {
            public string Email { get; set; }
            public string Token { get; set; }
        }
        public class Enable2FAModel
        {

            public string PhoneNumber { get; set; }
        }

        public class Verify2FAModel
        {
            public string Token { get; set; }
        }
    }
}