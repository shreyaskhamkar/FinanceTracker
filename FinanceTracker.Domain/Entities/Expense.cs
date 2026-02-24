namespace FinanceTracker.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public ExpenseCategory Category { get; private set; }

        private Expense() { } // EF Core

        public Expense(string title, decimal amount, DateTime date, ExpenseCategory category)
        {
            Id = Guid.NewGuid();
            Title = title;
            Amount = amount;
            Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            Category = category;
        }

        public void Update(string title, decimal amount, DateTime date, ExpenseCategory category)
        {
            Title = title;
            Amount = amount;
            Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            Category = category;
        }
    }


}
