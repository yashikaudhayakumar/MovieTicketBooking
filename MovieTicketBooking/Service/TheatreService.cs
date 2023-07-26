using AutoMapper;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Repository.Interface;
using MovieTicketBooking.Service.Interface;

namespace MovieTicketBooking.Business.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class TheatreService : ITheatreService
    {
        public readonly ITheatreRepository Repository;
        public readonly IMapper Mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public TheatreService(ITheatreRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Theatre>> GetTheatre()
        {
            return await Repository.GetTheatre();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Theatre> GetTheatre(string id)
        {
            return await Repository.GetTheatre(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CreateResponse> AddTheatre(TheatreDto data)
        {
            Theatre theatre = Mapper.Map<Theatre>(data);
            theatre.Created = DateTime.Now;
            theatre.Updated = DateTime.Now;

            return await Repository.AddTheatre(theatre);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CreateResponse> UpdateTheatre(TheatreDto data, string id)
        {
            Theatre theatre = Mapper.Map<Theatre>(data);
            theatre.Updated = DateTime.Now;

            return await Repository.UpdateTheatre(theatre, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CreateResponse> DeleteTheatre(string id)
        {
            return await Repository.DeleteTheatre(id);
        }
    }
}
