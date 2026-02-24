namespace FinanceTracker.Application.DTOs;
public record CategorySummaryDto(
    string Category,
    decimal TotalAmount
);