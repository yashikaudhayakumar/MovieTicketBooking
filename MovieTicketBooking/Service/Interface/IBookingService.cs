using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Service.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CreateResponse> TicketBook(TicketDto ticket, string userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Tickets>> ReteriveTicktes(string userId);
    }
}
