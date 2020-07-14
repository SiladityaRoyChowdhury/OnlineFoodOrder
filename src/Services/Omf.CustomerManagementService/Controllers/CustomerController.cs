using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Omf.CustomerManagementService.DomainModel;
using Omf.CustomerManagementService.Helpers;
using Omf.CustomerManagementService.Service;
using Omf.CustomerManagementService.ViewModel;

namespace Omf.CustomerManagementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerSettings _customerSettings;

        public CustomerController(ICustomerService customerService, IMapper mapper, ILogger<CustomerController> logger, IOptions<CustomerSettings> customerSettings)
        {
            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
            _customerSettings = customerSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserEmail) || string.IsNullOrEmpty(model.Password))
                    return null;
                var user = await _customerService.AuthenticateCustomer(model.UserEmail, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_customerSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.UserEmail),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    Id = user.UserId,
                    UserEmail = user.UserEmail,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenString
                });
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDetails model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Password))
                    throw new CustomerException("Password is required");

                if (_customerService.GetAllCustomers().Result.Any(x => x.UserEmail == model.UserEmail))
                    throw new CustomerException("userEmail \"" + model.UserEmail + "\" is already taken");

                var user = _mapper.Map<User>(model);
                user.UserId = System.Guid.NewGuid();
                await _customerService.CreateCustomer(user, model.Password);
                return Ok();
            }
            catch (CustomerException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var users = await _customerService.GetAllCustomers();
                var model = _mapper.Map<IList<UserModel>>(users);
                return Ok(model);
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCustomer(Guid userId, [FromBody] UserDetails model)
        {
            if (string.IsNullOrWhiteSpace(model.UserEmail))
            {
                throw new CustomerException("userEmail cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                throw new CustomerException("First name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                throw new CustomerException("Last name cannot be empty");
            }

            try
            {               
                var user = _mapper.Map<User>(model);
                user.UserId = userId;
                await _customerService.UpdateCustomer(user, model.Password);
                return Ok();
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                if (await _customerService.GetUserById(userId) == null)
                    throw new CustomerException("userId " + userId + " does not exists");
                await _customerService.DeleteCustomer(userId);
                return Ok();
            }
            catch(CustomerException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.InternalServerError);
            }
        }
    }
}
