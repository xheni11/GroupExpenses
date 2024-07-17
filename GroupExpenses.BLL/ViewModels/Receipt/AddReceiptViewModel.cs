using GroupExpenses.Enums;

namespace GroupExpenses.BLL.ViewModels.Receipt
{
    public record AddReceiptViewModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }
        public int PaidBy { get; set; }
        public IEnumerable<int> PaidFor { get; set; }

    }
}
