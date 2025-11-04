using Clean.Service;
using Microsoft.AspNetCore.Mvc;
using Clean.Core.DTOs;
using Clean.Core.Services;

namespace Clean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        public AuthController(JwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDTO loginDto)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find user by username (includes password for authentication)
            var user = _userService.GetByUsernameForAuth(loginDto.Username);
            
            // Verify user exists and password matches (Note: In production, use hashed passwords)
            if (user != null && user.Password == loginDto.Password)
            {
                // Generate JWT token with user ID as claim
                var token = _jwtService.GenerateToken(user.Id.ToString());
                
                // Create response DTO without password
                var userResponse = new UserResponseDTO 
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };
                
                // Return token and user info
                return Ok(new LoginResponseDTO 
                { 
                    Token = token.Token, 
                    User = userResponse 
                });
            }
            
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO registerDto)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if username already exists
            var existingUser = _userService.GetByUsername(registerDto.Username);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Username already exists" });
            }

            // Create new user
            var newUser = _userService.Add(registerDto);
            
            // Generate JWT token for immediate login
            var token = _jwtService.GenerateToken(newUser.Id.ToString());
            
            // Return token and user info
            return Ok(new LoginResponseDTO 
            { 
                Token = token.Token, 
                User = newUser 
            });
        }
    }
}