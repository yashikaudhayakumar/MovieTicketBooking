using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Service.Interface
{
    public interface IMovieService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        Task<CreateResponse> Create(MovieDto movie);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Movie>> GetMovie();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        Task<Movie> GetMovie(string movieId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        Task<CreateResponse> DeleteMovie(string movieId);
    }
}
