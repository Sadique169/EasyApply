using AutoMapper;
using EasyApply.Core.Domian;
using EasyApply.Dto;
using EasyApply.Model;
using EasyApply.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;        
        private readonly IMapper mapper;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;            
            this.mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.FullName,
                Email = model.Email,
                PasswordHash = model.Password,
                PhoneNumber = model.PhoneNumber,
                ProfilePicture = "",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response<RegisterModel>()
                {
                    Success = false,
                    ErrorCode = ErrorCode.ERROR_7,
                    Message = "",
                    Errors = AddErrors(result)
                });
            }

            //add user to role
            await userManager.AddToRoleAsync(user, model.RoleName);

            var createdUser = mapper.Map<ApplicationUserDto>(user);
            return StatusCode(StatusCodes.Status201Created, new Response<ApplicationUserDto>(createdUser));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return UserNotFound();

            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            if (string.IsNullOrWhiteSpace(token))
                return StatusCode(StatusCodes.Status400BadRequest, new Response<RegisterModel>()
                {
                    Success = false,
                    ErrorCode = ErrorCode.ERROR_12,
                    Message = "",
                    Errors = null
                });

            return StatusCode(StatusCodes.Status200OK, new Response<RegisterModel>()
            {
                Success = false,
                ErrorCode = ErrorCode.ERROR_0,
                Message = "Email sent successfully",
                Errors = null
            });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return UserNotFound();

            model.Token = model.Token.Replace(' ', '+');
            var res = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!res.Succeeded)
                return StatusCode(StatusCodes.Status400BadRequest, new Response<RegisterModel>()
                {
                    Success = false,
                    ErrorCode = ErrorCode.ERROR_11,
                    Message = "",
                    Errors = null
                });

            return StatusCode(StatusCodes.Status200OK, new Response<RegisterModel>()
            {
                Success = false,
                ErrorCode = ErrorCode.ERROR_0,
                Message = "Password updated successfully",
                Errors = null
            });
        }

        [NonAction]
        public ObjectResult UserNotFound()
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response<RegisterModel>()
            {
                Success = false,
                ErrorCode = ErrorCode.ERROR_5,
                Message = "",
                Errors = new List<string> { "Not found" }
            });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private List<string> AddErrors(IdentityResult result)
        {
            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            return errors;
        }
    }

}
