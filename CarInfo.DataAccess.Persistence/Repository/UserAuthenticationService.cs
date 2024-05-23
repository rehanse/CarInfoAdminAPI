using CarInfo.DataAccess.Domain.Identity;
using CarInfo.DataAccess.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
    public UserAuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this._configuration = configuration;
    }

    public async Task<ResponseStatus> RegisterAsync(RegisterModel model)
    {
        var status = new ResponseStatus();
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            status.status = "Error";
            status.message = "User already exists!";
            status.status = "InternalServerError";
            status.statusCode = 0;
        }

        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            status.status = "Error";
            status.message = "User creation failed! Please check user details and try again.";
            status.status = "InternalServerError";
            status.statusCode = 0;
        }
        if (UserRoles.Admin == model.Role)
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        else
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            await userManager.AddToRoleAsync(user, UserRoles.User);
        }
        status.status = "Success";
        status.message = "User created successfully!";
        status.status = "Ok";
        status.statusCode = 1;
        return status;
    }
    public async Task<ResponseStatus> LoginAsync(LoginModel model)
    {
        ResponseStatus status = new ResponseStatus();
        var user = await userManager.FindByNameAsync(model.UserName);
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

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            status.token = new JwtSecurityTokenHandler().WriteToken(token);
            status.status = "OK";
            status.statusCode = 1;
            status.message = "Login Successfully";
            status.role = userRoles.Contains(UserRoles.Admin) ? UserRoles.Admin : UserRoles.User;
            return status;
        }
        status.status = "Unauthorized";
        status.statusCode = 0;
        status.message = "Unauthorized login";
        return status;
    }


    public async Task<ResponseStatus> LogoutAsync()
    {
        ResponseStatus status = new ResponseStatus();
        //await signInManager.SignOutAsync();
        status.statusCode = 1;
        status.message = "Log Off";
        return status;

    }

}