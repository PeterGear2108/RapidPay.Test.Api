using Microsoft.IdentityModel.Tokens;
using RapidPay.Test.Api.Services.Interfaces;
using RapidPay.Test.DataAccess;
using RapidPay.Test.Models.Domain;
using RapidPay.Test.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace RapidPay.Test.Api.Services
{
    public class AuthService : Repository<User>, IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        
        public void CreateUser(string username, string password)
        {
            try
            {
                AddEntity(new User { Username = username, Password = EncryptPassword(password) });
                SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserToken Login(string username, string password)
        {
            try
            {
                var encryptedPassword = EncryptPassword(password);
                var user = _context.Users.FirstOrDefault(x => x.Username == username && x.Password == encryptedPassword);
                if (user == null)
                    throw new KeyNotFoundException("User does not exist");
                return GenerateUserToken(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserToken GenerateUserToken(User user)
        {
            var claims = new[]
              {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT")));
            var audienceToken = _configuration.GetValue<string>("JWT_AUDIENCE_TOKEN");
            var issuerToken = _configuration.GetValue<string>("JWT_ISSUER_TOKEN");
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(8);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: issuerToken,
               audience: audienceToken,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }

        public string EncryptPassword(string password)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
