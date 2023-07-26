using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MovieTicketBooking.Data;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;

namespace MovieTicketBooking.Business.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class TheatreRepository : ITheatreRepository
    {
        private readonly IMongoCollection<Theatre> _theatre;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="configuration"></param>
        public TheatreRepository(IDatabaseConnection settings, IConfiguration configuration)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            _theatre = database.GetCollection<Theatre>("Theatre");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Theatre>> GetTheatre()
        {
            return await _theatre.Find(t => true).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Theatre> GetTheatre(string id)
        {
            return await _theatre.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CreateResponse> AddTheatre(Theatre data)
        {
            CreateResponse response = new CreateResponse();
            try
            {
                await _theatre.InsertOneAsync(data);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CreateResponse> DeleteTheatre(string id)
        {
            CreateResponse response = new CreateResponse();

            try
            {
                await _theatre.DeleteOneAsync(t => t.Id == id);
                response.IsSuccess = true;
                response.Message = "Data deleted";
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
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CreateResponse> UpdateTheatre(Theatre data, string id)
        {
            CreateResponse response = new CreateResponse();
            try
            {
                await _theatre.ReplaceOneAsync(t => t.Id == id, data);
                response.IsSuccess = true;
                response.Message = "Data updated";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
