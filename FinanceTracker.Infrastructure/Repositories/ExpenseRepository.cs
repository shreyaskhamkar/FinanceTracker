using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly FinanceDbContext _context;

        public ExpenseRepository(FinanceDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        // READ ALL
        public async Task<List<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .AsNoTracking()
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        // READ BY ID
        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            Guid id,
            string title,
            decimal amount,
            DateTime date,
            ExpenseCategory category)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense is null)
                return false;

            expense.Update(title, amount, date, category);
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense is null)
                return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<MonthlySummaryDto>> GetMonthlySummaryAsync()
        {
            var data = await _context.Expenses.ToListAsync();

            return data
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new MonthlySummaryDto(
                    g.Key.Year,
                    g.Key.Month,
                    g.Sum(x => x.Amount)
                ))
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();
        }

        public async Task<List<CategorySummaryDto>> GetCategorySummaryAsync()
        {
            var data = await _context.Expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToListAsync();

            // Convert enum to string AFTER SQL execution
            return data
                .Select(x => new CategorySummaryDto(
                    x.Category.ToString(),
                    x.TotalAmount
                ))
                .ToList();
        }
        public async Task<List<Expense>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }
    }
}