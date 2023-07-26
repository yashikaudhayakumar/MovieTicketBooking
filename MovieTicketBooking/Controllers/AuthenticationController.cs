using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Business.Repository;
using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Service.Interface;

namespace MovieTicketBooking.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomerService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public AuthenticationController(ICustomerService service)
        {
            _service = service;
        }


        /// <summary>
        /// User login method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            CreateResponse response = await _service.CreateJSONToken(model);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
