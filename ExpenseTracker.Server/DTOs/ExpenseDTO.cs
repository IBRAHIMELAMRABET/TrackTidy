using ExpenseTracker.Server.Models;

namespace ExpenseTracker.Server.DTOs
{
    public class ReadExpenseDTO
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int CategoryId { get; set; }

        public CategoryReadDto Category { get; set; }

        public string? Notes { get; set; }
    }

    public class CreateExpenseDTO
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int CategoryId { get; set; }

        public string? Notes { get; set; } = null;
    }

    public class UpdateExpenseDTO
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime ExpenseDate { get; set; }

        public int CategoryId { get; set; }

        public string? Notes { get; set; } = null;
    }
}
