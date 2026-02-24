using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IExpenseRepository _repository;

    public AnalyticsController(IExpenseRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthlySummary()
    {
        var result = await _repository.GetMonthlySummaryAsync();
        return Ok(result);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategorySummary()
    {
        var result = await _repository.GetCategorySummaryAsync();
        return Ok(result);
    }
}