using FinanceTracker.Application.DTOs;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);
        Task<List<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, string title, decimal amount, DateTime date, ExpenseCategory category);
        Task<bool> DeleteAsync(Guid id);
        Task<List<MonthlySummaryDto>> GetMonthlySummaryAsync();
        Task<List<CategorySummaryDto>> GetCategorySummaryAsync();
    }
}
