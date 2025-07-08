using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.APIs.DTO;
using Web.APIs.Models;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTO registerDTO)
        {
            if(ModelState.IsValid)
            {
                //Save DB
                ApplicationUser user = new();
                user.Email = registerDTO.Email;
                user.UserName = registerDTO.UserName;
                IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
                if(result.Succeeded)
                    return Ok("Created");
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("Data", item.Description);
                }
            }
            return BadRequest(ModelState);  
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7068/",
                    audience: "https://localhost:7068/",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Keep it simple stupid 2003 Ziad Ghoraba 2003 @eng.ziadghoraba@gmail.com")),
                        SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }
    }
}
