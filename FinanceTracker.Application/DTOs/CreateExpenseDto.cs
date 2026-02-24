using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.DTOs
{
    public record CreateExpenseDto(
    string Title,
    decimal Amount,
    DateTime Date,
    int Category
);
}
