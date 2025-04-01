using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersTasks.Interfaces;
using UsersTasks.Models;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;
using UsersTasks.Models.Validations;

namespace UsersTasks.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [Authorize]
    public class UsersController : ApiController
    {
        IUsersService _service;
        public UsersController(IUsersService service) : base() { _service = service; }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser([Required] int id)
        {
            var result = await _service.GetUser(id);
            return SendApiResponse(result);
        }

        [HttpGet]
        public async Task<ActionResult<UsersResponse>> GetUsers(
            [FromQuery, GreaterThanZero] int page = 1, [FromQuery, GreaterThanZero] int pagesize = 10) {
            var result = await _service.GetUsers(page, pagesize);
            return SendApiResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> AddUser([FromBody] NewUser newUser) { 
            var resp = await _service.AddUser(newUser);
            return SendApiResponse(resp);
        }

    }
}
