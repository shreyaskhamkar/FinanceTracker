using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseRepository _repository;

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
    }
    public ExpensesController(IExpenseRepository repository)
    {
        _repository = repository;
    }

    // -------------------- CREATE --------------------
    [HttpPost]
    [Authorize]

    public async Task<IActionResult> Create(CreateExpenseDto dto)
    {
        var userId = GetUserId();

        var expense = new Expense(
            dto.Title,
            dto.Amount,
            DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
            dto.Category,
            userId
        );

        await _repository.AddAsync(expense);

        return Ok();
    }
    // -------------------- READ ALL --------------------
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var expenses = await _repository.GetByUserIdAsync(userId);
        return Ok(expenses);
    }
    // -------------------- READ BY ID --------------------
    [HttpGet("{id:guid}")]
    [Authorize]

    public async Task<IActionResult> GetById(Guid id)
    {
        var expense = await _repository.GetByIdAsync(id);
        if (expense is null)
            return NotFound();

        return Ok(expense);
    }

    // -------------------- UPDATE --------------------
    [HttpPut("{id:guid}")]
    [Authorize]

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
    [Authorize]

    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}