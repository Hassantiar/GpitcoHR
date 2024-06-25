using Base.DTOs;
using Base.Entites;
using Base.Responses;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serverlibrary.Data;
using Serverlibrary.Helper;
using Serverlibrary.Repository.Contact;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serverlibrary.Repository.Implemintation
{
    public class UserAccountRepository(IOptions<Jwtsection> cofig, AppDBContext appDBContext) : IUserAccount
    {
        public async Task<GeneralRespons> CreateAsync(Reguster user)
        {
            if (user is null) return new GeneralRespons(false, "Model is Empty");
            var checkuser = await FindUserByEmail(user.Email);
            if (checkuser != null) return new GeneralRespons(false, "user register Already");
            var applicationuser = await AddToDatabase(new ApplicationUser()
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });
            var chechadminrole = await appDBContext.SystemRoles.FirstOrDefaultAsync(r => r.name!.Equals(Cosntants.Admin));
            if (chechadminrole is null)
            {
                var createadminrole = await AddToDatabase(new SystemRoles() { name = Cosntants.Admin });
                await AddToDatabase(new UserRole() { RoleId = createadminrole.id, UserId = applicationuser.Id });
                return new GeneralRespons(true, "Account Created !!");
            }
            var checkuserrloe = await appDBContext.SystemRoles.FirstOrDefaultAsync(s => s.name!.Equals(Cosntants.User));
            SystemRoles respons = new();
            if (checkuserrloe is null)
            {
                respons = await AddToDatabase(new SystemRoles()
                {
                    name = Cosntants.User
                });
                await AddToDatabase(new UserRole() { RoleId = respons.id, UserId = applicationuser.Id });
            }
            else
            {
                await AddToDatabase(new UserRole() { RoleId = checkuserrloe.id, UserId = applicationuser.Id });
            }
            return new GeneralRespons(true, "Account Created !!");
        }

        public async Task<LoginRespons> SingInAsync(Login user)
        {
            if (user is null) return new LoginRespons(false, "Model is empty");
            var loginuser = await FindUserByEmail(user.Email!);

            if (loginuser is null) return new LoginRespons(false, "User not Found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, loginuser.Password))
                return new LoginRespons(true, "Email/Password Not Valid");

            var userrol = await FinfUserRole(loginuser.Id);
            if (userrol is null) return new LoginRespons(false, "user role not found");

            var userrolname = await FindRoleName(userrol.RoleId);
            if (userrol is null) return new LoginRespons(false, "user role not found");

            string jwtToken = GeneratToken(loginuser, userrolname!.name);
            string refrshToken = RefreshToken();
            ///Save token in sql server 
            var finduser = await appDBContext.RefrasgTokenInfo.FirstOrDefaultAsync(x => x.userid == loginuser.Id);
            if (finduser is not null)
            {
                finduser!.Token = refrshToken;
                await appDBContext.SaveChangesAsync();
            }
            else
            {
                await AddToDatabase(new RefrasgTokenInfo()
                {
                    Token = refrshToken,
                    userid = loginuser.Id
                });
            }

            return new LoginRespons(true, "Successfully loging", jwtToken, refrshToken);
        }
        private string GeneratToken(ApplicationUser user, string Role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cofig.Value.Key!));
            var Credintioal = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role,Role!)
            };
            var Token = new JwtSecurityToken(
                issuer: cofig.Value.Issuer,
                audience: cofig.Value.Audience,
                claims: userClim,
                expires: DateTime.Now.AddSeconds(2),
                signingCredentials: Credintioal
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public static string RefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await appDBContext.ApplicationUsers.FirstOrDefaultAsync(a => a.Email!.ToLower()!.Equals(email!.ToLower()));
        private async Task<T> AddToDatabase<T>(T modle)
        {
            var result = appDBContext.Add(modle!);
            await appDBContext.SaveChangesAsync();
            return (T)result.Entity;

        }
        private async Task<UserRole> FinfUserRole(int userId) => await appDBContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        private async Task<SystemRoles> FindRoleName(int roleid) => await appDBContext.SystemRoles.FirstOrDefaultAsync(x => x.id == roleid);
        public async Task<LoginRespons> RefrashInfoAsync(RefreshToken Token)
        {
            if (Token is null) return new LoginRespons(false, "Model is empty");

            var refreshtoken = await appDBContext.RefrasgTokenInfo.FirstOrDefaultAsync(x => x.Token!.Equals(Token.Token));
            if (refreshtoken is null) return new LoginRespons(false, "refresh token is requerd");

            var user = await appDBContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == refreshtoken.userid);
            if (user is null) return new LoginRespons(false, "erfresh Token cannot generat becouse user can not found");

            var userrole = await FinfUserRole(user.Id);
            var rolename = await FindRoleName(userrole.RoleId);
            string jwtToken = GeneratToken(user, rolename.name);
            string refresh = RefreshToken();
            var updatetoke = await appDBContext.RefrasgTokenInfo.FirstOrDefaultAsync(x => x.userid == user.Id);
            if (updatetoke is null) return new LoginRespons(false, "erfresh Token cannot generate becuse use not singin ");

            updatetoke.Token = refresh;
            await appDBContext.SaveChangesAsync();
            return new LoginRespons(true, "Token Refreshed succfully ", jwtToken, refresh);
        }
    }
}
