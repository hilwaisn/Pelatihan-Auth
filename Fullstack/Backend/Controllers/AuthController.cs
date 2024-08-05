using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Helpers;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwt;
        public AuthController(IUserRepository repository, JwtService jwt)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Success");
        }

        //https://localhost:8000/api/register
        [HttpPost("register")]
        public IActionResult CreateUser(RegistrasiDto dto)
        {
            //pemetaas objek
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                //Password = dto.Password
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            return Created("success", _repository.Create(user));
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _repository.GetByEmail(dto.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credential" });
            }

            var jwt = _jwt.Generate(user.Id);
            return Ok("Anda berhasil masuk");
        }
    }
}