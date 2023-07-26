using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Business.Repository;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        /// <summary>
        /// Constructor of UserController
        /// </summary>
        /// <param name="service"></param>
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Provide valid data");
            }

            CreateResponse response = await _service.CreateUser(model);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Update the user password
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update/password")]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword([FromBody] UserPasswordUpdate userPassword)
        {
            string username = User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid)
            {
                return BadRequest("No user found");
            }

            CreateResponse response = await _service.PasswordUpdate(userPassword, username);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
