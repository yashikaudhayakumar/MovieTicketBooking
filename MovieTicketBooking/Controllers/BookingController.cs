using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Service.Interface;
using System.Security.Claims;

namespace MovieTicketBooking.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "CustomerOnly")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        /// <summary>
        /// Constructor of Ticket Controller
        /// </summary>
        /// <param name="service"></param>
        public BookingController(IBookingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Book an movie ticket for user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Book")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TicketBook([FromBody] TicketDto model)
        {
            string userId = User.FindFirstValue("Id");
            CreateResponse response = await _service.TicketBook(model, userId);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(List<Tickets>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReteriveTickets()
        {
            string userId = User.FindFirstValue("Id");

            List<Tickets> response = await _service.ReteriveTicktes(userId);

            return response.Count > 0 ? Ok(response) : NotFound();
        }
    }
}
