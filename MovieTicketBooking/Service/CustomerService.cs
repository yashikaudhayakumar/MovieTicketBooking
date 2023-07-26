using AutoMapper;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;
using MovieTicketBooking.Service.Interface;
using System.Security.Cryptography;

namespace MovieTicketBooking.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public async Task<CreateResponse> CreateUser(UserDto data, bool isAdmin = false)
        {
            User user = _mapper.Map<User>(data);
            user.IsAdmin = isAdmin;
            user.Created = DateTime.Now;
            user.Updated = DateTime.Now;

            CreatePasswordHash(data.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return await _repository.CreateUser(user);
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<CreateResponse> PasswordUpdate(UserPasswordUpdate userPassword, string username)
        {
            User currentUser = await _repository.GetUserByUsername(username);

            if (currentUser != null)
            {
                return await _repository.PasswordUpdate(userPassword, currentUser);
            }
            var response = new CreateResponse()
            {
                IsSuccess = false,
                Message = "New password not matched"
            };
            return response;
        }

        /// <summary>
        /// Create a JSON token for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<CreateResponse> CreateJSONToken(AuthenticationRequest user)
        {
            CreateResponse response = new CreateResponse();

            if (await _repository.CheckUserExistsByUsername(user.Username))
            {
                User currentUser = await _repository.GetUserByUsername(user.Username);

                if (await _repository.VerifyUserPassword(user.Password, currentUser))
                {
                    string token = _repository.GenerateToken(currentUser, currentUser.IsAdmin ? "Admin" : "Customer");

                    response.IsSuccess = true;
                    response.Message = token;
                    return response;
                }
                response.IsSuccess = false;
                response.Message = "Password not match";
                return response;
            }
            response.IsSuccess = false;
            response.Message = "No user found";
            return response;
        }


        /// <summary>
        /// Verify password with hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> VerifyUserPassword(string password, User user)
        {
            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        /// <summary>
        /// Create hash and salt for passowrd
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Verify password with hashed password using salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
