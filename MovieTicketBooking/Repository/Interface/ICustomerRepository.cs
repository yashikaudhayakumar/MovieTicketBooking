using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;

namespace MovieTicketBooking.Repository.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<CreateResponse> CreateUser(User data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> CheckUserExistsByUsername(string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetUserByUsername(string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> VerifyUserPassword(string password, User user);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<CreateResponse> PasswordUpdate(UserPasswordUpdate userPassword, User username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string GenerateToken(User user, string role);
    }
}
