using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersTasks.Data;
using UsersTasks.Interfaces;
using UsersTasks.Models.Auth;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;
using UsersTasks.Services;

namespace UsersTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {

        private readonly IAuthService _service; // new c


        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult> AuthLogin(Login login)
        {
            var resp = await _service.AuthLogin(login);
            return SendApiResponse(resp);
        }
    }
}
