using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Models;

namespace ExpenseTracker.Server.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ReadExpenseDTO>> GetExpensesAsync(int userId);
        Task<ReadExpenseDTO> GetExpenseAsync(int id, int userId);
        Task<ReadExpenseDTO> CreateExpenseAsync(CreateExpenseDTO expenseDto, int userId);
        Task UpdateExpenseAsync(int id, CreateExpenseDTO expenseDto, int userId);
        Task DeleteExpenseAsync(int id, int userId);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly ExpenseTrackerDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseService(ExpenseTrackerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadExpenseDTO>> GetExpensesAsync(int userId)
        {
            var expenses = await _context.Expenses.Where(c => c.UserId == userId)
                .Include(e => e.Category)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ReadExpenseDTO>>(expenses);
        }

        public async Task<ReadExpenseDTO> GetExpenseAsync(int id, int userId)
        {
            var expense = await _context.Expenses.Where(c => c.Id == id && c.UserId == userId)
                .Include(e => e.Category)
                .FirstOrDefaultAsync();
            if (expense == null)
            {
                return null;
            }
            return _mapper.Map<ReadExpenseDTO>(expense);
        }

        public async Task<ReadExpenseDTO> CreateExpenseAsync(CreateExpenseDTO expenseDto, int userId)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            expense.UserId = userId;
            Console.WriteLine($"Received CategoryId: {expense}");


            if (expense.ExpenseDate == default)
            {
                expense.ExpenseDate = DateTime.UtcNow;
            }

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadExpenseDTO>(expense);
        }

        public async Task UpdateExpenseAsync(int id, CreateExpenseDTO expenseDto, int userId)
        {
            var existingExpense = await _context.Expenses.Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync(); ;

            if (existingExpense == null)
            {
                throw new KeyNotFoundException($"Expense with id {id} not found.");
            }

            _mapper.Map(expenseDto, existingExpense);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    throw new KeyNotFoundException($"Expense with id {id} not found.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteExpenseAsync(int id, int userId)
        {
            var expense = await _context.Expenses.Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync(); ;

            if (expense == null)
            {
                throw new KeyNotFoundException($"Expense with id {id} not found.");
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
