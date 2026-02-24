namespace FinanceTracker.Application.DTOs;

public record MonthlySummaryDto(
    int Year,
    int Month,
    decimal TotalAmount
);
