using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;

namespace MovieTicketBooking.Service.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITheatreService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Theatre>> GetTheatre();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Theatre> GetTheatre(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<CreateResponse> AddTheatre(TheatreDto data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<CreateResponse> UpdateTheatre(TheatreDto data, string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CreateResponse> DeleteTheatre(string id);
    }
}
