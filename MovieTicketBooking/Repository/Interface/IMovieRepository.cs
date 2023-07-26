using MovieTicketBooking.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTicketBooking.Data.Models.Entities;

namespace MovieTicketBooking.Repository.Interface
{
    public interface IMovieRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        Task<CreateResponse> Create(Movie movie);

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
