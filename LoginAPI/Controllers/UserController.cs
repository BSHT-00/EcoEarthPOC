using LoginAPI.Data;
using LoginAPI.DTOs;
using LoginAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LoginAPI.Context;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailServcie;
        public UsersController(UserManager<Users> userManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _emailServcie = emailService;
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var userToBeCreated = new Users
            {
                Email = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                UserName = registerUserDTO.Email
            };

            var response = await _userManager.CreateAsync(userToBeCreated, registerUserDTO.Password);

            if (response.Succeeded)
            {
                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userToBeCreated);
                var confirmationLink = $"yourapp://confirmemail?userId={userToBeCreated.Id}&token={WebUtility.UrlEncode(token)}";

                // Send email
                await _emailServcie.SendConfirmationEmailAsync(userToBeCreated.Email, confirmationLink);

                return Ok(new { isSuccess = true, errorMessage = string.Empty, content = userToBeCreated });
            }

            var errors = response.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { isSuccess = false, errorMessage = errors, content = (object)null });
        }


        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUser authenticateUser)
        {
            var user = await _userManager.FindByNameAsync(authenticateUser.Email);
            if (user == null) return Unauthorized();

            bool isValidUser = await _userManager.CheckPasswordAsync(user, authenticateUser.Password);

            if (isValidUser)
            {
               var tokenHandler = new JwtSecurityTokenHandler();

                var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} { user.LastName}"),
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _configuration["JWT:Audience"],
                    Issuer = _configuration["JWT:Issuer"],
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }

        }

        
        // Returns a UserIdDTO, which only contains the userId currently using email
        [AllowAnonymous]
        [HttpGet("GetUser/{Email}")]
        public async Task<IActionResult> GetUser(string Email)
        {
            var user = await _userManager.FindByNameAsync(Email);

            if (user == null) 
                return NotFound();

            var userIdDTO = new userIdDTO
            {
                UserId = user.Id
            };

            return Ok(userIdDTO);
        }


        //[AllowAnonymous]
        //   [HttpPost("ResetPassword")]
        //   public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        //  {
        //   var user = await _userManager.FindByEmailAsync(model.Email);
        // if (user == null)
        // {
        // Don't reveal that the user does not exist
        //  return Ok(new { Message = "Password reset successfully" });
        //   }

        //       var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        //       if (result.Succeeded)
        //       {
        //           return Ok(new { Message = "Password reset successfully" });
        //       }

        //      return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        //   }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully.");
            }

            return BadRequest("Failed to change password.");
        }


        //    [AllowAnonymous]
        //    [HttpGet("ConfirmEmail")]
        //    public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //    {
        //        if (userId == null || token == null)
        //        {
        //            return BadRequest("Invalid parameters");
        //        }

        //        var user = await _userManager.FindByIdAsync(userId);
        //        if (user == null)
        //        {
        //            return BadRequest("User not found");
        //        }

        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return Ok("Email confirmed successfully");
        //        }

        //        return BadRequest("Email confirmation failed");
        //    }
    }
}

