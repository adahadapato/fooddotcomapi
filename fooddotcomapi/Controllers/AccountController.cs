using fooddotcomapi.Dto;
using fooddotcomapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace fooddotcomapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _repository;
        public AccountController(IAccountService repository)
        {
            _repository = repository;
        }



        /// <summary>
        /// Confirm email address
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("confirmemail/{token}/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _repository.ConfirmEmail(token, email);
            if (result.IsSuccess)
                return Ok(new { Message = result.ErrorMessage });
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await _repository.ResetPassword(model.Email, model.Code, model.Password);
            if (result.IsSuccess)
                return Ok(new { Message = result.ErrorMessage });
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }

        /// <summary>
        /// Frogot Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgotpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var result = await _repository.ForgotPassword(model.Email);
            if (result.IsSuccess)
                return Ok(new { Message = result.ErrorMessage });
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseDto))]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _repository.Login(model);
            if (result.IsSuccess)
                return Ok(result.result);
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }

        /// <summary>
        /// Register new users
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _repository.Register(model);
            if (result.IsSuccess)
                return Ok(new { Message = result.ErrorMessage });
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }

        /// <summary>
        /// Update User account details with new names
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("updatename")]
        [Authorize(Roles = "Admin, User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameDto model)
        {
            var result = await _repository.UpdateName(model);
            if (result.IsSuccess)
                return Ok(new { Message = result.ErrorMessage });
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }

        /// <summary>
        /// Updates User Phone number
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("updatephone")]
        [Authorize(Roles = "Admin, User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdatePhoneDto model)
        {
            var result = await _repository.UpdatePhone(model);
            if (result.IsSuccess)
                return Ok(new { Message = result.ErrorMessage });
            else
                return BadRequest(new { Message = result.ErrorMessage });
        }


        /// <summary>
        /// Delete account
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("delete/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string email)
        {
            var (_success, _msg) = await _repository.Delete(email);
            if (_success)
                return Ok(new { message = _msg });
            else
                return BadRequest(_msg);
        }
    }
}
