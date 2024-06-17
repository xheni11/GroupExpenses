using System.ComponentModel.DataAnnotations.Schema;

namespace GroupExpenses.Domain.Entities
{
   public class Receipt
   {
         public int Id { get; set; }
         public int EventId { get; set; }
         [ForeignKey(nameof(EventId))]
         public Event Event { get; set; }
         public string Name { get; set; }
         public decimal Price { get; set; }
         public decimal PriceInEur { get; set; }
         public string Details { get; set; }
         public int Currency { get; set; }
         public User PaidBy { get; set; }
         public IEnumerable<User> PaidFor { get; set; }
   }
}
