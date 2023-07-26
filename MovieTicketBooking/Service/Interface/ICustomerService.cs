using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;

namespace MovieTicketBooking.Service.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        Task<CreateResponse> CreateUser(UserDto data, bool isAdmin = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<CreateResponse> PasswordUpdate(UserPasswordUpdate userPassword, string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<CreateResponse> CreateJSONToken(AuthenticationRequest user);
    }
}
