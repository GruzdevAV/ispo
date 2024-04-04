using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DrivingSchoolAPIModels;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для неавторизованного пользователя
    /// </summary>
    [Route("api")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            // Если пользователь не найден или пароль не совпадает, то отмена авторизации 
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new Response
                {
                    Status = "Login denied",
                    Message = user == null ? "Пользователь не найден" : "Неверный пароль"
                });
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            // Создать токен
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            var role = userRoles.First();
            return Ok(new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Role = role,
                Id = user.Id
            });
        }
        /// <summary>
        /// Проверить, работает ли сервер.
        /// А также создать администратора по умолчанию, если его ещё нет.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Ping")]
        public async Task<IActionResult> CreateDefaultAdmin()
        {
            var userExists = await _userManager.FindByEmailAsync(DefaultData.AdminEmail);
            // Если администратор уже есть, то ответить на "пинг" словом "понг" 
            if (userExists != null)
                return Ok(new Response { Status = "Success", Message = "Pong" });
            // В другом случае - создать администратора
            ApplicationUser user = new()
            {
                UserName = DefaultData.AdminName,
                Email = DefaultData.AdminEmail,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(user, DefaultData.AdminPassword);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "Не удалось создать администратора."
                    });
            // Создать каждую роль, если она ещё не создана
            foreach (var role in new[] { UserRoles.Admin, UserRoles.Student, UserRoles.Instructor })
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            // Назначить нового администратора администратором
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            return Ok(new Response { Status = "Success", Message = "Администратор успешно создан!" });
        }

    }
}
