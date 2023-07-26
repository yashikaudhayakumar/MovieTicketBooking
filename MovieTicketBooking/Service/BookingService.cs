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
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public BookingService(IMapper mapper, IBookingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tickets>> ReteriveTicktes(string userId)
        {
            return await _repository.ReteriveTicktes(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CreateResponse> TicketBook(TicketDto ticket, string userId)
        {
            Tickets ticketBook = _mapper.Map<Tickets>(ticket);
            ticketBook.UserId = userId;
            ticketBook.Created = DateTime.Now;
            ticketBook.Updated = DateTime.Now;

            return await _repository.TicketBook(ticketBook);
        }
    }
}
