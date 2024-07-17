using GroupExpenses.BLL.ViewModels.User;
using GroupExpenses.Enums;

namespace GroupExpenses.BLL.ViewModels.Receipt
{
    public record GetReceiptViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceInEur { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }
        public GetUserViewModel PaidBy { get; set; }
        public IEnumerable<GetUserViewModel> PaidFor { get; set; }

    }
}
