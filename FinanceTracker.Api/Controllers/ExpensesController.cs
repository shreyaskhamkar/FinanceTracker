using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseRepository _repository;

    public ExpensesController(IExpenseRepository repository)
    {
        _repository = repository;
    }

    // -------------------- CREATE --------------------
    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseDto dto)
    {
        var expense = new Expense(
            dto.Title,
            dto.Amount,
            dto.Date,
            (ExpenseCategory)dto.Category
        );

        await _repository.AddAsync(expense);
        return CreatedAtAction(nameof(GetById), new { id = expense.Id }, expense);
    }

    // -------------------- READ ALL --------------------
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _repository.GetAllAsync();
        return Ok(expenses);
    }

    // -------------------- READ BY ID --------------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var expense = await _repository.GetByIdAsync(id);
        if (expense is null)
            return NotFound();

        return Ok(expense);
    }

    // -------------------- UPDATE --------------------
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateExpenseDto dto)
    {
        var updated = await _repository.UpdateAsync(
            id,
            dto.Title,
            dto.Amount,
            dto.Date,
            (ExpenseCategory)dto.Category
        );

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // -------------------- DELETE --------------------
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}