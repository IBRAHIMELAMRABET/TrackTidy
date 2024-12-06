using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReadExpenseDTO>>> GetExpenses()
        {
            var userId = GetUserIdFromClaims();
            var expenses = await _expenseService.GetExpensesAsync(userId);
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ReadExpenseDTO>> GetExpense(int id)
        {
            var userId = GetUserIdFromClaims();
            var expense = await _expenseService.GetExpenseAsync(id, userId);

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReadExpenseDTO>> CreateExpense(CreateExpenseDTO expenseDto)
        {
            var userId = GetUserIdFromClaims();
            var createdExpense = await _expenseService.CreateExpenseAsync(expenseDto, userId);

            return CreatedAtAction(
                nameof(GetExpense),
                new { id = createdExpense.Id },
                createdExpense
            );
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateExpense(int id, CreateExpenseDTO expenseDto)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                await _expenseService.UpdateExpenseAsync(id, expenseDto,userId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                await _expenseService.DeleteExpenseAsync(id, userId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
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
