using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Repository.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        Task<CreateResponse> TicketBook(Tickets ticket);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Tickets>> ReteriveTicktes(string userId);
    }
}
