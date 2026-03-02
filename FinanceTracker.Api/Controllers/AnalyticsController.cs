using FinanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/analytics")]
[Authorize] // 🔐 Protect everything
public class AnalyticsController : ControllerBase
{
    private readonly IExpenseRepository _repository;

    public AnalyticsController(IExpenseRepository repository)
    {
        _repository = repository;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthlySummary()
    {
        var userId = GetUserId();

        var result = await _repository.GetMonthlySummaryAsync(userId);

        return Ok(result);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategorySummary()
    {
        var userId = GetUserId();

        var result = await _repository.GetCategorySummaryAsync(userId);

        return Ok(result);
    }
}