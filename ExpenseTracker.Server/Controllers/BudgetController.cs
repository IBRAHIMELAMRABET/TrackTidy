using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApp.Services;
using ExpenseTracker.Server.DTOs;

namespace ExpenseTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetsController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateBudget([FromBody] BudgetDTO budgetDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var budget = await _budgetService.AddOrUpdateBudget(budgetDto.UserId, budgetDto.TotalAmount);
            return Ok(budget);
        }

        [HttpGet("{userId}")]
        public IActionResult GetBudget(int userId)
        {
            var budget = _budgetService.GetBudgetByUserId(userId);
            if (budget == null)
                return NotFound("Budget not found for the current month.");

            return Ok(budget);
        }
    }
}
