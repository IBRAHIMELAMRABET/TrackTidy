using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var authResponse = await _authService.RegisterAsync(registerDto);

                _logger.LogInformation($"User registered: {registerDto.Username}");

                return CreatedAtAction(nameof(Register), authResponse);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning($"Registration failed: {ex.Message}");

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during registration");

                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var authResponse = await _authService.LoginAsync(loginDto);

                _logger.LogInformation($"User logged in: {loginDto.UsernameOrEmail}");

                return Ok(authResponse);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning($"Login failed: {ex.Message}");

                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login");

                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    return BadRequest(new { message = "Refresh token is required." });
                }

                var authResponse = await _authService.RefreshTokenAsync(refreshToken);

                _logger.LogInformation("Token refreshed successfully");

                return Ok(authResponse);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning($"Token refresh failed: {ex.Message}");

                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during token refresh");

                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}