using AutoMapper;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;
using MovieTicketBooking.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of Movie Service
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public MovieService(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task<CreateResponse> Create(MovieDto movie)
        {
            Movie movieData = _mapper.Map<Movie>(movie);
            movieData.Created = DateTime.Now;
            movieData.Updated = DateTime.Now;

            return await _repository.Create(movieData);
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<Movie>> GetMovie()
        {
            return await _repository.GetMovie();
        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<Movie> GetMovie(string movieId)
        {
            return await _repository.GetMovie(movieId);
        }

        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<CreateResponse> DeleteMovie(string movieId)
        {
            return await _repository.DeleteMovie(movieId);
        }
    }
}
