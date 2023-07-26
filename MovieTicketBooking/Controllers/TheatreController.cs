using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Service.Interface;

namespace MovieTicketBooking.Controllers
{
    /// <summary>
    /// Constructor of Theatre Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreService service;

        /// <summary>
        /// Constructor of Theatre Controller
        /// </summary>
        public TheatreController(ITheatreService _service)
        {
            service = _service;
        }

        /// <summary>
        /// Get list of theatre
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/All")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTheatre()
        {
            List<Theatre> theatre = await service.GetTheatre();

            return theatre.Count > 0 ? Ok(theatre) : BadRequest(theatre);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTheatre([FromRoute] string id)
        {
            Theatre theatre = await service.GetTheatre(id);

            return theatre != null ? Ok(theatre) : BadRequest(theatre);
        }
    }
}
