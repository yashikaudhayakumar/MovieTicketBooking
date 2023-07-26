using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieTicketBooking.Data;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieTicketBooking.Business.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="configuration"></param>
        public CustomerRepository(IDatabaseConnection settings, IConfiguration configuration)
        {
            _configuration = configuration;
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("User");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CreateResponse> CreateUser(User data)
        {
            CreateResponse response = new CreateResponse();
            if (await _user.Find(u => u.Username == data.Username).AnyAsync())
            {
                response.IsSuccess = false;
                response.Message = "username already exists";
                return response;
            }
            try
            {
                await _user.InsertOneAsync(data);
                response.IsSuccess = true;
                response.Message = "Data inserted";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserExistsByUsername(string username)
        {
            return await _user.Find(u => u.Username == username).AnyAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsername(string username)
        {
            return await _user.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<CreateResponse> PasswordUpdate(UserPasswordUpdate userPassword, User user)
        {
            CreateResponse response = new CreateResponse();

            if (await VerifyUserPassword(userPassword.OldPassword, user))
            {
                if (userPassword.NewPassword == userPassword.ConfirmPassword)
                {
                    CreatePasswordHash(userPassword.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    await _user.ReplaceOneAsync(u => u.Id == user.Id, user);

                    response.IsSuccess = true;
                    response.Message = "Password updated";
                    return response;
                }
                response.IsSuccess = false;
                response.Message = "new password and confirm password not match";
                return response;

            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Old password not match";
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> VerifyUserPassword(string password, User user)
        {
            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GenerateToken(User user, string role)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Username),
                new Claim(ClaimTypes.Role, role),
                new Claim("AccessLevel", role)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
