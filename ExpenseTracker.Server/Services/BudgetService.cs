using ExpenseTracker.Data;
using ExpenseTracker.Server.Models;


namespace ExpenseTrackerApp.Services
{
    public class BudgetService
    {
        private readonly ExpenseTrackerDbContext _context;

        public BudgetService(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Budget> AddOrUpdateBudget(int userId, decimal totalAmount)
        {
            var budget = _context.Budgets.FirstOrDefault(b => b.UserId == userId && b.Month.Month == DateTime.Now.Month);

            if (budget == null)
            {
                budget = new Budget
                {
                    UserId = userId,
                    TotalAmount = totalAmount,
                    RemainingAmount = totalAmount
                };
                _context.Budgets.Add(budget);
            }
            else
            {
                budget.TotalAmount = totalAmount;
                budget.RemainingAmount = Math.Max(0, totalAmount - (budget.TotalAmount - budget.RemainingAmount));
            }

            await _context.SaveChangesAsync();
            return budget;
        }

        public Budget GetBudgetByUserId(int userId)
        {
            return _context.Budgets.FirstOrDefault(b => b.UserId == userId && b.Month.Month == DateTime.Now.Month);
        }

        public async Task DeductExpense(int userId, decimal amount)
        {
            var budget = GetBudgetByUserId(userId);
            if (budget != null)
            {
                budget.RemainingAmount -= amount;
                await _context.SaveChangesAsync();
            }
        }
    }
}
