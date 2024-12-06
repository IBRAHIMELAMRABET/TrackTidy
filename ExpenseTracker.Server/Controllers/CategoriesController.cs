using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize] 
        public async Task<IActionResult> GetAllCategories()
        {
            var userId = GetUserIdFromClaims();
            var categories = await _categoryService.GetAllCategoriesAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCategory(int id)
        {
            var userId = GetUserIdFromClaims();
            var category = await _categoryService.GetCategoryByIdAsync(id, userId);
            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto createDto)
        {
            var userId = GetUserIdFromClaims();
            var category = await _categoryService.CreateCategoryAsync(createDto, userId);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto updateDto)
        {
            var userId = GetUserIdFromClaims();
            var category = await _categoryService.UpdateCategoryAsync(id, updateDto, userId);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = GetUserIdFromClaims();
            await _categoryService.DeleteCategoryAsync(id, userId);
            return NoContent();
        }

        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
