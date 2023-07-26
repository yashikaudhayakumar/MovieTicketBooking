using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models.Entities;
using MovieTicketBooking.Service.Interface;

namespace MovieTicketBooking.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public MovieController(IMovieService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new Movie
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMovie(MovieDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            CreateResponse response = await _service.Create(model);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// .
        /// <returns></returns>
        [HttpGet]
        [Route("Reterive/All")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMovie()
        {
            List<Movie> movies = await _service.GetMovie();
            return movies.Count > 0 ? Ok(movies) : BadRequest(movies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Reterive/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMovie([FromRoute] string id)
        {
            Movie movies = await _service.GetMovie(id);
            return movies != null ? Ok(movies) : BadRequest(movies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMovie([FromRoute] string id)
        {
            CreateResponse response = await _service.DeleteMovie(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
