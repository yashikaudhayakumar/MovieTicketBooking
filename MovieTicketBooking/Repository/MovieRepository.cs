using MongoDB.Driver;
using MovieTicketBooking.Data;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movie;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public MovieRepository(IDatabaseConnection settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            _movie = database.GetCollection<Movie>("Movie");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task<CreateResponse> Create(Movie movie)
        {
            CreateResponse response = new CreateResponse();
            try
            {
                await _movie.InsertOneAsync(movie);
                response.IsSuccess = true;
                response.Message = "Movie created";
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
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<CreateResponse> DeleteMovie(string movieId)
        {
            CreateResponse response = new CreateResponse();
            try
            {
                await _movie.DeleteOneAsync(m => m.Id == movieId);
                response.IsSuccess = true;
                response.Message = "Movie deleted";
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
        /// <returns></returns>
        public async Task<List<Movie>> GetMovie()
        {
            return await _movie.Find(m => true).Sort("{Created:-1}").ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<Movie> GetMovie(string movieId)
        {
            return await _movie.Find(m => m.Id == movieId).FirstOrDefaultAsync();
        }
    }
}
